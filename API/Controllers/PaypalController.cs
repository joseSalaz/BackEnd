using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using Models.RequestResponse;
using IService;
using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using IRepository;
using Repository;
using Bussines;
using IBussines;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.CustomUI;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        private readonly IApisPaypalServices _apisPaypalServices;
        private readonly IConfiguration _configuration;
        private readonly IKardexRepository _kardexRepository;
        private readonly IKardexBussines _kardexBussines;
        private readonly IDetalleVentaBussines _IDetalleVentaBussines = null;
        private readonly IVentaBussines _IVentaBussines = null;
        private readonly ICajaBussines _ICajaBussines;
        private readonly ICajaRepository _ICajaRepository;
        private readonly IMapper _mapper;
        public PaypalController(IApisPaypalServices apisPaypalServices, IConfiguration configuration, IKardexRepository kardexRepository, IKardexBussines kardexBussines, IMapper mapper, IDetalleVentaBussines detalleVentaBussines, IVentaBussines ventaBussines, ICajaBussines iCajaBussines, ICajaRepository iCajaRepository)
        {
            _apisPaypalServices = apisPaypalServices;
            _configuration = configuration;
            _kardexRepository = kardexRepository;
            _kardexBussines = kardexBussines;
            _mapper = mapper;
            _IDetalleVentaBussines = detalleVentaBussines;
            _IVentaBussines = ventaBussines;
            _ICajaBussines = iCajaBussines;
            _ICajaRepository = iCajaRepository;
        }



        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreationRequest paymentRequest)
        {
            try
            {
                // Suponiendo que `DatalleCarrito` se construye correctamente desde el `paymentRequest`
                DatalleCarrito detalleCarrito = new DatalleCarrito
                {
                    // Construir el detalle del carrito aquí
                };
                string returnUrl = $"{_configuration["http://localhost:4200"]}/respuesta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige después del pago exitoso
                string cancelUrl = $"{_configuration["http://localhost:4200"]}/respuesta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige si el usuario cancela el pago
                var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, paymentRequest.Amount, returnUrl, cancelUrl);
                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
                if (string.IsNullOrWhiteSpace(approvalUrl))
                {
                    return BadRequest("No se pudo obtener la URL de aprobación de PayPal.");
                }
                return Ok(new { PaymentId = payment.id, ApprovalUrl = approvalUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno al crear el pago: " + ex.Message);
            }
        }



        [HttpPost("execute-payment")]
        public async Task<IActionResult> ExecutePayment([FromBody] ExecutePaymentModelRequest paymentRequest)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(
                _configuration["PayPalSettings:ClientId"],
                _configuration["PayPalSettings:Secret"]
            ).GetAccessToken());
            var paymentExecution = new PaymentExecution { payer_id = paymentRequest.PayerID };
            var payment = new Payment { id = paymentRequest.PaymentId };
            try
            {
                var executedPayment = payment.Execute(apiContext, paymentExecution);
                if (executedPayment.state.ToLower() == "approved")
                {
                    await RegistrarVentaYDetalle(paymentRequest);
                    // No necesitas llamar a SaveChanges si tu método Update ya lo hace internamente
                    return Ok(new { PaymentId = executedPayment.id });
                }
                else
                {
                    return BadRequest("El pago no fue aprobado.");
                }
            }
            catch (Exception ex)
            {
                // Registro de errores y respuesta adecuada
                return StatusCode(500, "Error al ejecutar el pago: " + ex.Message);
            }
        }


        //private async Task<IActionResult> ProcesarPagoEnEfectivo(ExecutePaymentModelRequest paymentRequest)
        //{
        //    await RegistrarVentaYDetalle(paymentRequest);
        //    return Ok("Procesado con Exito");
        //}

        private async Task<IActionResult> RegistrarVentaYDetalle(ExecutePaymentModelRequest paymentRequest)
        {
            string numeroComprobante = await _IVentaBussines.GenerarNumeroComprobante();

            var cajaDelDia = _ICajaBussines.RegistrarVentaEnCajaDelDia();
            if (cajaDelDia == null)
            {
                return BadRequest("Es necesario abrir una caja para hoy antes de registrar ventas.");
            }
            // Extraer y preparar la información de la venta a partir del paymentRequest
            VentaRequest ventaRequest = new VentaRequest
            {
                FechaVenta = DateTime.Now,
                TipoComprobante = "Boleta",
                IdUsuario = 1, // Usar el UserId desde el paymentRequest si está disponible
                NroComprobante = numeroComprobante,
                IdPersona = paymentRequest.Carrito.Persona.IdPersona,
                TotalPrecio = paymentRequest.Carrito.TotalAmount, // Asegúrate de que existe un campo Total en ExecutePaymentModelRequest
                IdCaja= cajaDelDia.IdCaja

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