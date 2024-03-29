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
                    return BadRequest("El carrito está vacío o es inválido");

                decimal total = detalleCarrito.Items.Sum(item => item.PrecioVenta * item.Cantidad);
                var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, total, "http://localhost:4200/detalle-venta", "http://localhost:4200/detalle-venta");


                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel == "http://localhost:4200/inicio")?.href;

                if (string.IsNullOrEmpty(approvalUrl))
                    return BadRequest("No se pudo obtener la URL de aprobación de PayPal");

                return Ok(new { PaymentId = payment.id, ApprovalUrl = approvalUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
