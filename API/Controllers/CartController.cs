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

        public CartController(IApisPaypalServices apisPaypalServices)
        {
            _apisPaypalServices = apisPaypalServices;
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
                var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, total, "https://libreriasaber.com/detalle-venta", "https://libreriasaber.com/detalle-venta");
                
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

    }
}
