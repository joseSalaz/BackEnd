using IService;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        private readonly IApisPaypalServices _apisPaypalServices;
        public PaypalController(IApisPaypalServices apisPaypalServices)
        {
            _apisPaypalServices = apisPaypalServices;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreationRequest paymentRequest)
        {
            {
                try
                {
                    DatalleCarrito detalleCarrito = new DatalleCarrito
                    {
                     
                    };
                    string frontEndBaseUrl = "http://localhost:4200";
                    string returnUrl = $"{frontEndBaseUrl}/inicio";
                    string cancelUrl = $"{frontEndBaseUrl}/detalle-venta";

                    var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, paymentRequest.Amount, returnUrl, cancelUrl);
                    var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
                    // Asegúrate de que approvalUrl no sea nulo o vacío antes de enviar la respuesta
                    if (string.IsNullOrWhiteSpace(approvalUrl))
                    {
                        throw new Exception("No se pudo obtener la URL de aprobación.");
                    }
                    return Ok(new { PaymentId = payment.id, ApprovalUrl = approvalUrl });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Error interno al crear el pago: " + ex.Message);
                }
            }

        }
    }
}


