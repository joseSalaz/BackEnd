using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IApisPaypalServices _apisPaypalServices;
        private readonly IPaymentService _apisMercadoPagoService;


        public CartController(IApisPaypalServices apisPaypalServices, IPaymentService apisMercadoPagoService)
        {
            _apisPaypalServices = apisPaypalServices;
            _apisMercadoPagoService = apisMercadoPagoService;
        }


        [HttpPost]
        public async Task<IActionResult> PostCart([FromBody] DatalleCarrito detalleCarrito)
        {

            try
            {
                if (detalleCarrito == null || !detalleCarrito.Items.Any())
                {
                    return BadRequest("El carrito está vacío o es inválido");
                }
                
                // Calcula el total del carrito basado en los items que contiene    
                decimal total = detalleCarrito.Items.Sum(item => item.PrecioVenta * item.Cantidad);

                // Crea el pedido en PayPal
                var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, total, "https://libreriasaber.store/detalle-venta", "https://libreriasaber.store/detalle-venta");
                
                // Busca la URL de aprobación para redirigir al usuario para el pago
                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel == "approval_url")?.href;
                if (string.IsNullOrEmpty(approvalUrl))
                {
                    return BadRequest("No se pudo obtener la URL de aprobación de PayPal");
                }
                // Retorna el ID del pago y la URL de aprobación para uso del frontend
                return Ok(new { PaymentId = payment.id, ApprovalUrl = approvalUrl});
            }
            //pendiente enviar id del cliente  a paypal controller para el manejo de esta
            catch (Exception ex)
            {
                // Manejo de excepciones con respuesta adecuada
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPost("mercadopago")]
        public async Task<IActionResult> PostCartMercadoPago([FromBody] DatalleCarrito detalleCarrito)
        {
            try
            {
                if (detalleCarrito == null || !detalleCarrito.Items.Any())
                {
                    return BadRequest("El carrito está vacío o es inválido");
                }

                decimal total = detalleCarrito.Items.Sum(item => item.PrecioVenta * item.Cantidad);

                // Llama al servicio para crear la preferencia de pago en Mercado Pago
                var urlDePago = await _apisMercadoPagoService.CreatePaymentAsync(detalleCarrito, total);

                if (string.IsNullOrEmpty(urlDePago))
                {
                    return BadRequest("No se pudo generar la URL de pago de Mercado Pago");
                }

                // Retorna la URL de pago de Mercado Pago al frontend
                return Ok(new { UrlDePago = urlDePago });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

    }
}
