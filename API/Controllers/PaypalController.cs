using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using Models.RequestResponse;
using IService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        private readonly IApisPaypalServices _apisPaypalServices;
        private readonly IConfiguration _configuration;

        public PaypalController(IApisPaypalServices apisPaypalServices, IConfiguration configuration)
        {
            _apisPaypalServices = apisPaypalServices;
            _configuration = configuration;
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

                string returnUrl = $"{_configuration["http://localhost:4200"]}/detalle-venta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige después del pago exitoso
                string cancelUrl = $"{_configuration["http://localhost:4200"]}/detalle-venta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige si el usuario cancela el pago

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
        public async Task<IActionResult> ExecutePayment(ExecutePaymentModelRequest paymenRequest)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(
                _configuration["PayPalSettings:ClientId"],
                _configuration["PayPalSettings:Secret"]
            ).GetAccessToken());

            var paymentExecution = new PaymentExecution { payer_id = paymenRequest.PayerID };
            var payment = new Payment { id = paymenRequest.PaymentId };

            try
            {
                var executedPayment =  payment.Execute(apiContext, paymentExecution);
                // Aquí deberías verificar el estado del pago en `executedPayment`
                // y realizar cualquier lógica de negocio necesaria, como actualizar el estado de la orden, etc.

                return Ok(new { executedPayment.id });
            }
            catch (Exception ex)
            {
                // Registrar el error y proporcionar una respuesta adecuada.
                return StatusCode(500, "Error al ejecutar el pago: " + ex.Message);
            }
        }
    }
}


