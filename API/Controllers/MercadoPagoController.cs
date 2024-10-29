using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using IService;
using IBussines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRepository;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IPaymentService _apisMercadoPagoService;
        private readonly IConfiguration _configuration;
        private readonly IKardexRepository _kardexRepository;
        private readonly IVentaBussines _IVentaBussines;
        private readonly ICajaBussines _ICajaBussines;
        private readonly IDetalleVentaBussines _IDetalleVentaBussines;
        private readonly ICajaRepository _ICajaRepository;

        public MercadoPagoController(IPaymentService apisMercadoPagoService, IConfiguration configuration, IKardexRepository kardexRepository, IVentaBussines ventaBussines, ICajaBussines iCajaBussines, IDetalleVentaBussines detalleVentaBussines, ICajaRepository iCajaRepository)
        {
            _apisMercadoPagoService = apisMercadoPagoService;
            _configuration = configuration;
            _kardexRepository = kardexRepository;
            _IVentaBussines = ventaBussines;
            _ICajaBussines = iCajaBussines;
            _IDetalleVentaBussines = detalleVentaBussines;
            _ICajaRepository = iCajaRepository;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreationRequest paymentRequest)
        {
            try
            {
                if (paymentRequest == null || !paymentRequest.Carrito.Items.Any())
                {
                    return BadRequest("El carrito está vacío o es inválido");
                }

                decimal total = paymentRequest.Carrito.Items.Sum(item => item.PrecioVenta * item.Cantidad);

                // Crear la preferencia de pago en Mercado Pago
                var urlDePago = await _apisMercadoPagoService.CreatePaymentAsync(paymentRequest.Carrito, total);

                if (string.IsNullOrEmpty(urlDePago))
                {
                    return BadRequest("No se pudo generar la URL de pago de Mercado Pago");
                }

                // Retornar la URL de pago
                return Ok(new { UrlDePago = urlDePago });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno al crear el pago: " + ex.Message);
            }
        }

        [HttpPost("execute-payment")]
        public async Task<IActionResult> ExecutePayment([FromBody] ExecuteMercadopagoRequest paymentRequest)
        {
            try
            {
                long paymentIdLong = long.Parse(paymentRequest.PaymentId); // Conversión de string a long

                // Lógica para procesar el pago aquí
                var paymentClient = new PaymentClient();
                var paymentResponse = await paymentClient.GetAsync(paymentIdLong); // Asegúrate de que este método acepte long

                // Verifica el estado del pago
                if (paymentResponse.Status == PaymentStatus.Approved)
                {
                    // El pago fue exitoso, registra la venta
                    await RegistrarVentaYDetalle(paymentRequest);
                    return Ok(new { Message = "Pago procesado con éxito." });
                }
                else
                {
                    return BadRequest(new { Message = "El pago no fue aprobado.", Status = paymentResponse.Status });
                }
            }
            catch (FormatException)
            {
                return BadRequest("El PaymentId proporcionado no es válido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al ejecutar el pago: " + ex.Message);
            }
        }




        private async Task<IActionResult> RegistrarVentaYDetalle(ExecuteMercadopagoRequest paymentRequest)
        {
            string numeroComprobante = await _IVentaBussines.GenerarNumeroComprobante();

            var cajaDelDia = _ICajaBussines.RegistrarVentaEnCajaDelDia();
            if (cajaDelDia == null)
            {
                return BadRequest("Es necesario abrir una caja para hoy antes de registrar ventas.");
            }

            VentaRequest ventaRequest = new VentaRequest
            {
                FechaVenta = DateTime.Now,
                TipoComprobante = "Boleta",
                IdUsuario = 1, // Usar el UserId desde el paymentRequest si está disponible
                NroComprobante = numeroComprobante,
                IdPersona = paymentRequest.Carrito.Persona.IdPersona,
                TotalPrecio = paymentRequest.Carrito.TotalAmount, // Asegúrate de que existe un campo Total en ExecutePaymentModelRequest
                IdCaja = cajaDelDia.IdCaja
            };

            var venta = _IVentaBussines.Create(ventaRequest);
            if (venta == null || venta.IdVentas <= 0)
            {
                return StatusCode(500, "Error al crear la venta");
            }
            _ICajaRepository.Update(cajaDelDia);

            List<DetalleVentaRequest> listaDetalle = new List<DetalleVentaRequest>();
            foreach (var item in paymentRequest.Carrito.Items)
            {
                var kardexActual = _kardexRepository.GetById(item.libro.IdLibro);
                if (kardexActual == null || kardexActual.Stock < item.Cantidad)
                {
                    return BadRequest("No hay suficiente stock para el libro con ID " + item.libro.IdLibro);
                }

                kardexActual.Stock -= item.Cantidad; // Asegúrate de que esto no ponga el stock en negativo
                _kardexRepository.Update(kardexActual); // Utiliza tu método Update del repositorio

                DetalleVentaRequest detalleVentaRequest = new DetalleVentaRequest
                {
                    IdVentas = venta.IdVentas,
                    NombreProducto = item.libro.Titulo,
                    PrecioUnit = item.PrecioVenta,
                    IdLibro = item.libro.IdLibro,
                    Cantidad = item.Cantidad,
                    Importe = item.PrecioVenta * item.Cantidad,
                    Estado = "Reservado" // Añadir el estado si es relevante
                };
                listaDetalle.Add(detalleVentaRequest);
            }

            var result = _IDetalleVentaBussines.CreateMultiple(listaDetalle);
            if (listaDetalle == null || !listaDetalle.Any())
            {
                return StatusCode(500, "Error al crear el detalle de la venta");
            }
            try
            {
                // Asumimos que el correo del cliente se puede obtener de paymentRequest o de otra fuente relevante
                string emailCliente = paymentRequest.Carrito.Persona.Correo; // Asegúrate de obtener el correo electrónico correctamente
                await _IVentaBussines.GenerarYEnviarPdfDeVenta(venta.IdVentas, emailCliente);
                return Ok(new { Message = "Venta y detalles registrados con éxito, correo enviado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "La venta se registró, pero el correo con el PDF no se pudo enviar: " + ex.Message);
            }

           
        }
    }
}
