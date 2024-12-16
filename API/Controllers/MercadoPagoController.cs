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
using Bussines;

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
        private readonly IEstadoPedidoBussines _IEstadoPedidoBussines;

        public MercadoPagoController(IPaymentService apisMercadoPagoService, IConfiguration configuration,
            IKardexRepository kardexRepository, IVentaBussines ventaBussines, ICajaBussines iCajaBussines, 
            IDetalleVentaBussines detalleVentaBussines, ICajaRepository iCajaRepository, IEstadoPedidoBussines iEstadoPedidoBussines)
        {
            _apisMercadoPagoService = apisMercadoPagoService;
            _configuration = configuration;
            _kardexRepository = kardexRepository;
            _IVentaBussines = ventaBussines;
            _ICajaBussines = iCajaBussines;
            _IDetalleVentaBussines = detalleVentaBussines;
            _ICajaRepository = iCajaRepository;
            _IEstadoPedidoBussines = iEstadoPedidoBussines;
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
            // Generar el número de comprobante
            string numeroComprobante = await _IVentaBussines.GenerarNumeroComprobante();

            // Registrar venta en caja del día
            var cajaDelDia = _ICajaBussines.RegistrarVentaEnCajaDelDia();
            if (cajaDelDia == null)
            {
                return BadRequest("Es necesario abrir una caja para hoy antes de registrar ventas.");
            }

            // Crear la venta a partir de la solicitud de pago de Mercado Pago
            VentaRequest ventaRequest = new VentaRequest
            {
                FechaVenta = DateTime.Now,
                TipoComprobante = "Boleta",
                IdUsuario = 1, // Usar el UserId desde paymentRequest si está disponible
                NroComprobante = numeroComprobante,
                IdPersona = paymentRequest.Carrito.Persona.IdPersona, // Persona del carrito
                TotalPrecio = paymentRequest.Carrito.TotalAmount, // Asegúrate de que existe TotalAmount en ExecuteMercadopagoRequest
                IdCaja = cajaDelDia.IdCaja
            };

            // Crear la venta
            var venta = _IVentaBussines.Create(ventaRequest);
            if (venta == null || venta.IdVentas <= 0)
            {
                return StatusCode(500, "Error al crear la venta");
            }

            // Actualizar caja del día
            _ICajaRepository.Update(cajaDelDia);

            // Lista de detalles de venta
            List<DetalleVentaRequest> listaDetalle = new List<DetalleVentaRequest>();
            foreach (var item in paymentRequest.Carrito.Items)
            {
                var kardexActual = _kardexRepository.GetById(item.libro.IdLibro);
                if (kardexActual == null || kardexActual.Stock < item.Cantidad)
                {
                    return BadRequest("No hay suficiente stock para el libro con ID " + item.libro.IdLibro);
                }

                // Actualizar el stock del libro
                kardexActual.Stock -= item.Cantidad;
                _kardexRepository.Update(kardexActual);

                // Crear el detalle de la venta
                DetalleVentaRequest detalleVentaRequest = new DetalleVentaRequest
                {
                    IdVentas = venta.IdVentas,
                    NombreProducto = item.libro.Titulo,
                    PrecioUnit = item.PrecioVenta,
                    IdLibro = item.libro.IdLibro,
                    Cantidad = item.Cantidad,
                    Importe = item.PrecioVenta * item.Cantidad,
                    Estado = "Reservado"
                };
                listaDetalle.Add(detalleVentaRequest);
            }

            // Crear los detalles de venta de manera masiva
            var result = _IDetalleVentaBussines.CreateMultiple(listaDetalle);
            if (result == null || !result.Any())
            {
                return StatusCode(500, "Error al crear el detalle de la venta");
            }

            // Asignar los IdDetalleVentas manualmente si es necesario
            for (int i = 0; i < listaDetalle.Count; i++)
            {
                listaDetalle[i].IdDetalleVentas = result[i].IdDetalleVentas; // Asignar el IdDetalleVentas devuelto
            }

            // Llamada al método RegistrarEstadoPedido después de haber creado el detalle de la venta
            var estadoPedidoResult = await RegistrarEstadoPedido(listaDetalle);
            if (estadoPedidoResult is ObjectResult objectResult && objectResult.StatusCode != 200)
            {
                return objectResult; // Propagar el error si ocurre durante el registro del estado del pedido
            }

            // Enviar PDF de venta por correo electrónico
            try
            {
                string emailCliente = paymentRequest.Carrito.Persona.Correo; // Asegúrate de obtener el correo correctamente
                await _IVentaBussines.GenerarYEnviarPdfDeVenta(venta.IdVentas, emailCliente);
                return Ok(new { Message = "Venta y detalles registrados con éxito, correo enviado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "La venta se registró, pero el correo con el PDF no se pudo enviar: " + ex.Message);
            }
        }

        private async Task<IActionResult> RegistrarEstadoPedido(List<DetalleVentaRequest> listaDetalle)
        {
            // Verificar que la lista de detalles no esté vacía
            if (listaDetalle == null || !listaDetalle.Any())
            {
                return BadRequest("La lista de detalles de venta está vacía.");
            }

            // Asegurarse de que cada detalle tenga un IdDetalleVentas asignado
            foreach (var detalle in listaDetalle)
            {
                if (detalle.IdDetalleVentas == 0)
                {
                    return StatusCode(500, "El detalle de venta con ID 0 no tiene un IdDetalleVentas asignado.");
                }
            }

            // Ahora que tenemos los detalles creados, asignamos los IdDetalleVentas y creamos el estado del pedido
            foreach (var detalle in listaDetalle)
            {
                EstadoPedidoRequest estadoPedidoRequest = new EstadoPedidoRequest
                {
                    IdDetalleVentas = detalle.IdDetalleVentas, // Usar el IdDetalleVentas del detalle
                    Estado = "Pedido Realizado",  // Estado inicial
                    FechaEstado = DateTime.Now,
                    Comentario = "Pedido realizado exitosamente." // Puedes agregar más detalles si lo necesitas
                };

                // Registrar el estado del pedido en la base de datos
                var estadoPedido = _IEstadoPedidoBussines.Create(estadoPedidoRequest);
                if (estadoPedido == null)
                {
                    return StatusCode(500, "Error al crear el estado del pedido para el detalle de la venta con ID " + detalle.IdDetalleVentas);
                }
            }

            // Devolver respuesta exitosa
            return Ok(new { Message = "Estado del pedido registrado correctamente para todos los detalles de la venta." });
        }




    }
}

