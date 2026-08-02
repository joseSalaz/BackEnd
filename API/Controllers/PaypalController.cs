using IBussines;
using IBussnies;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using PayPal.Api;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PaypalController : ControllerBase
  {
    private readonly IApisPaypalServices _apisPaypalServices;
    private readonly IConfiguration _configuration;
    private readonly IVentaBussines _ventaBussines;
    private readonly IOrderMesageFirebase _orderMesageFirebase;
    private readonly IUsuarioBussnies _usuarioBussnies;

    public PaypalController(
        IApisPaypalServices apisPaypalServices,
        IConfiguration configuration,
        IVentaBussines ventaBussines,
        IOrderMesageFirebase orderMesageFirebase,
        IUsuarioBussnies usuarioBussnies)
    {
      _apisPaypalServices = apisPaypalServices;
      _configuration = configuration;
      _ventaBussines = ventaBussines;
      _orderMesageFirebase = orderMesageFirebase;
      _usuarioBussnies = usuarioBussnies;
    }

    [HttpPost("create-payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentCreationRequest paymentRequest)
    {
      try
      {
        DatalleCarrito detalleCarrito = new DatalleCarrito { };

        string returnUrl = $"{_configuration[" http://localhost:4200/inicio "]}/respuesta";
        string cancelUrl = $"{_configuration[" http://localhost:4200/inicio "]}/respuesta";
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

        if (executedPayment.state.ToLower() != "approved")
        {
          return BadRequest("El pago no fue aprobado.");
        }

        var options = new ConfirmarPagoOptions
        {
          ActualizarSaldoCaja = true,
          EstadoPedidoInicial = "En Proceso"
        };

        var resultado = await _ventaBussines.ConfirmarPagoYActualizarStockAsync(paymentRequest.Carrito, options);

        await NotificarNuevoPedidoAsync();

        try
        {
          string emailCliente = paymentRequest.Carrito.Persona.Correo;
          await _ventaBussines.GenerarYEnviarPdfDeVenta(resultado.Venta.IdVentas, emailCliente);
        }
        catch (Exception)
        {
          return StatusCode(500, "La venta se registró, pero el correo con el PDF no se pudo enviar.");
        }

        return Ok(new { PaymentId = executedPayment.id, VentaId = resultado.Venta.IdVentas });
      }
      catch (InvalidOperationException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception)
      {
        return StatusCode(500, new { success = false, message = "Error al ejecutar el pago." });
      }
    }

    private async Task NotificarNuevoPedidoAsync()
    {
      var deviceTokens = await _usuarioBussnies.GetNotificationTokensAsync();
      foreach (var token in deviceTokens)
      {
        await _orderMesageFirebase.SendFirebaseNotificationAsync(token, "Nuevo Pedido", "¡Tienes un nuevo pedido en la app!");
      }
    }
  }
}
