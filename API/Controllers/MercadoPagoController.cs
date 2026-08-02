using IBussines;
using IBussnies;
using IService;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MercadoPagoController : ControllerBase
  {
    private readonly IPaymentService _apisMercadoPagoService;
    private readonly IVentaBussines _ventaBussines;
    private readonly IOrderMesageFirebase _orderMesageFirebase;
    private readonly IUsuarioBussnies _usuarioBussnies;

    public MercadoPagoController(
        IPaymentService apisMercadoPagoService,
        IVentaBussines ventaBussines,
        IOrderMesageFirebase orderMesageFirebase,
        IUsuarioBussnies usuarioBussnies)
    {
      _apisMercadoPagoService = apisMercadoPagoService;
      _ventaBussines = ventaBussines;
      _orderMesageFirebase = orderMesageFirebase;
      _usuarioBussnies = usuarioBussnies;
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
        var urlDePago = await _apisMercadoPagoService.CreatePaymentAsync(paymentRequest.Carrito, total);

        if (string.IsNullOrEmpty(urlDePago))
        {
          return BadRequest("No se pudo generar la URL de pago de Mercado Pago");
        }

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
        long paymentIdLong = long.Parse(paymentRequest.PaymentId);
        var paymentClient = new PaymentClient();
        var paymentResponse = await paymentClient.GetAsync(paymentIdLong);

        if (paymentResponse.Status != PaymentStatus.Approved)
        {
          return BadRequest(new { Message = "El pago no fue aprobado.", Status = paymentResponse.Status });
        }

        var options = new ConfirmarPagoOptions
        {
          IdCaja = 4,
          ActualizarSaldoCaja = false,
          EstadoPedidoInicial = "Pedido Realizado"
        };

        var resultado = await _ventaBussines.ConfirmarPagoYActualizarStockAsync(paymentRequest.Carrito, options);

        await NotificarNuevoPedidoAsync();

        try
        {
          string emailCliente = paymentRequest.Carrito.Persona.Correo;
          await _ventaBussines.GenerarYEnviarPdfDeVenta(resultado.Venta.IdVentas, emailCliente);
        }
        catch (Exception ex)
        {
          return StatusCode(500, "La venta se registró, pero el correo con el PDF no se pudo enviar: " + ex.Message);
        }

        return Ok(new { Message = "Pago procesado con éxito.", VentaId = resultado.Venta.IdVentas });
      }
      catch (FormatException)
      {
        return BadRequest("El PaymentId proporcionado no es válido.");
      }
      catch (InvalidOperationException ex)
      {
        return BadRequest(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(500, "Error al ejecutar el pago: " + ex.Message);
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
