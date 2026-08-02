<<<<<<< HEAD
using IBussines;
using IBussnies;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using PayPal.Api;

=======
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
using IBussnies;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PaypalController : ControllerBase
  {
    private readonly IApisPaypalServices _apisPaypalServices;
    private readonly IConfiguration _configuration;
<<<<<<< HEAD
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

=======
    private readonly IKardexRepository _kardexRepository;
    private readonly IKardexBussines _kardexBussines;
    private readonly IDetalleVentaBussines _IDetalleVentaBussines = null;
    private readonly IVentaBussines _IVentaBussines = null;
    private readonly ICajaBussines _ICajaBussines;
    private readonly ICajaRepository _ICajaRepository;
    private readonly IMapper _mapper;
    private readonly IEstadoPedidoBussines _IEstadoPedidoBussines;
    private readonly IOrderMesageFirebase _IOrderMesageFirebase;
    private readonly IUsuarioBussnies _IUsuarioBussnies;
    private readonly LibreriaSaberContext _dbContext;

    public PaypalController(IApisPaypalServices apisPaypalServices, IConfiguration configuration, IKardexRepository kardexRepository,
            IKardexBussines kardexBussines, IMapper mapper, IDetalleVentaBussines detalleVentaBussines, IVentaBussines ventaBussines,
            ICajaBussines iCajaBussines, ICajaRepository iCajaRepository, IEstadoPedidoBussines estadoPedidoBussines, IOrderMesageFirebase orderMesageFirebase,
            IUsuarioBussnies usuarioBussnies, LibreriaSaberContext dbContext)
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
      _IEstadoPedidoBussines = estadoPedidoBussines;
      _IOrderMesageFirebase = orderMesageFirebase;
      _IUsuarioBussnies = usuarioBussnies;
      _dbContext = dbContext;

    }



>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
    [HttpPost("create-payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentCreationRequest paymentRequest)
    {
      try
      {
<<<<<<< HEAD
        DatalleCarrito detalleCarrito = new DatalleCarrito { };

        string returnUrl = $"{_configuration[" http://localhost:4200/inicio "]}/respuesta";
        string cancelUrl = $"{_configuration[" http://localhost:4200/inicio "]}/respuesta";
=======
        // Suponiendo que `DatalleCarrito` se construye correctamente desde el `paymentRequest`
        DatalleCarrito detalleCarrito = new DatalleCarrito
        {

        };

        string returnUrl = $"{_configuration[" http://localhost:4200/inicio "]}/respuesta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige después del pago exitoso
        string cancelUrl = $"{_configuration[" http://localhost:4200/inicio "]}/respuesta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige si el usuario cancela el pago
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, paymentRequest.Amount, returnUrl, cancelUrl);

        var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
        if (string.IsNullOrWhiteSpace(approvalUrl))
        {
          return BadRequest("No se pudo obtener la URL de aprobación de PayPal.");
        }
<<<<<<< HEAD

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        return Ok(new { PaymentId = payment.id, ApprovalUrl = approvalUrl });
      }
      catch (Exception ex)
      {
        return StatusCode(500, "Error interno al crear el pago: " + ex.Message);
      }
    }

<<<<<<< HEAD
    [HttpPost("execute-payment")]
    public async Task<IActionResult> ExecutePayment([FromBody] ExecutePaymentModelRequest paymentRequest)
    {
      var apiContext = new APIContext(new OAuthTokenCredential(
          _configuration["PayPalSettings:ClientId"],
          _configuration["PayPalSettings:Secret"]
      ).GetAccessToken());

      var paymentExecution = new PaymentExecution { payer_id = paymentRequest.PayerID };
      var payment = new Payment { id = paymentRequest.PaymentId };

=======


    [HttpPost("execute-payment")]
    // Ejecutando el pago con la información del carrito
    public async Task<IActionResult> ExecutePayment([FromBody] ExecutePaymentModelRequest paymentRequest)

    {
      // Asegúrate de que el idDireccion esté presente en el carrito del paymentRequest
      var apiContext = new APIContext(new OAuthTokenCredential(
                 _configuration["PayPalSettings:ClientId"],
                 _configuration["PayPalSettings:Secret"]
             ).GetAccessToken());

      var paymentExecution = new PaymentExecution { payer_id = paymentRequest.PayerID };
      var payment = new Payment { id = paymentRequest.PaymentId };
      Console.WriteLine("PaymentId: " + paymentRequest.PaymentId);
      Console.WriteLine("PayerID: " + paymentRequest.PayerID);
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
      try
      {
        var executedPayment = payment.Execute(apiContext, paymentExecution);

<<<<<<< HEAD
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
=======
        if (executedPayment.state.ToLower() == "approved")
        {
          // Registrar la venta y detalle con el idDireccion correctamente
          var resultadoVenta = await RegistrarVentaYDetalle(paymentRequest); // Aquí pasamos el idDireccion
          if (resultadoVenta is not OkObjectResult)
            return resultadoVenta;
          Console.WriteLine(resultadoVenta);
          return Ok(new { PaymentId = executedPayment.id });

        }
        else
        {
          return BadRequest("El pago no fue aprobado.");
        }
      }
      catch (Exception ex)
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
      {
        return StatusCode(500, new { success = false, message = "Error al ejecutar el pago." });
      }
    }

<<<<<<< HEAD
    private async Task NotificarNuevoPedidoAsync()
    {
      var deviceTokens = await _usuarioBussnies.GetNotificationTokensAsync();
      foreach (var token in deviceTokens)
      {
        await _orderMesageFirebase.SendFirebaseNotificationAsync(token, "Nuevo Pedido", "¡Tienes un nuevo pedido en la app!");
      }
=======


    //private async Task<IActionResult> ProcesarPagoEnEfectivo(ExecutePaymentModelRequest paymentRequest)
    //{
    //    await RegistrarVentaYDetalle(paymentRequest);
    //    return Ok("Procesado con Exito");
    //}

    private async Task<IActionResult> RegistrarVentaYDetalle(ExecutePaymentModelRequest paymentRequest)
    {
      // Iniciar transacción
      using var transaction = await _dbContext.Database.BeginTransactionAsync();

      try
      {
        string numeroComprobante = await _IVentaBussines.GenerarNumeroComprobante();

        var idCaja = _ICajaBussines.RegistrarventasEcomerce();
        if (idCaja == null)
          return BadRequest("Es necesario abrir una caja para hoy antes de registrar ventas.");

        VentaRequest ventaRequest = new VentaRequest
        {
          FechaVenta = DateTime.Now,
          TipoComprobante = "Boleta",
          IdUsuario = 1,
          NroComprobante = numeroComprobante,
          IdPersona = paymentRequest.Carrito.Persona.IdPersona,
          TotalPrecio = paymentRequest.Carrito.TotalAmount,
          IdCaja = idCaja.IdCaja,
          IdDireccion = paymentRequest.Carrito.IdDireccion
        };

        var venta = _IVentaBussines.Create(ventaRequest);
        if (venta == null || venta.IdVentas <= 0)
          return StatusCode(500, "Error al crear la venta");
        idCaja.SaldoFinal += paymentRequest.Carrito.TotalAmount;
        _ICajaRepository.Update(idCaja);

        List<DetalleVentaRequest> listaDetalle = new List<DetalleVentaRequest>();

        foreach (var item in paymentRequest.Carrito.Items)
        {
          var kardexActual = _kardexRepository.GetById(item.libro.IdLibro);
          if (kardexActual == null || kardexActual.Stock < item.Cantidad)
          {
            // Al retornar BadRequest aquí, el catch de abajo NO se ejecuta
            // así que hacemos rollback explícito
            await transaction.RollbackAsync();
            return BadRequest("No hay suficiente stock para el libro con ID " + item.libro.IdLibro);
          }

          kardexActual.Stock -= item.Cantidad;
          _kardexRepository.Update(kardexActual);

          listaDetalle.Add(new DetalleVentaRequest
          {
            IdVentas = venta.IdVentas,
            NombreProducto = item.libro.Titulo,
            PrecioUnit = item.PrecioVenta,
            IdLibro = item.libro.IdLibro,
            Cantidad = item.Cantidad,
            Importe = item.PrecioVenta * item.Cantidad,
            Estado = "Reservado"
          });
        }

        var result = _IDetalleVentaBussines.CreateMultiple(listaDetalle);
        if (result == null || !result.Any())
        {
          await transaction.RollbackAsync();
          return StatusCode(500, "Error al crear el detalle de la venta");
        }

        for (int i = 0; i < listaDetalle.Count; i++)
          listaDetalle[i].IdDetalleVentas = result[i].IdDetalleVentas;

        var estadoPedidoResult = await RegistrarEstadoPedido(listaDetalle);
        if (estadoPedidoResult is ObjectResult obj && obj.StatusCode != 200)
        {
          await transaction.RollbackAsync();
          return obj;
        }

        // ✅ Todo salió bien → confirmar cambios en BD
        await transaction.CommitAsync();
        //string emailCliente = paymentRequest.Carrito.Persona.Correo;
        //await _IVentaBussines.GenerarYEnviarPdfDeVenta(venta.IdVentas, emailCliente);
        return Ok(new { Message = "Venta, detalles y estado registrados con éxito." });
      }
      catch (Exception ex)
      {
        // ❌ Cualquier excepción → revertir TODO
        await transaction.RollbackAsync();
        return StatusCode(500, "Error al registrar la venta: " + ex.Message);
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
          Estado = "En Proceso",  // Estado inicial
          FechaEstado = DateTime.Now,
          Comentario = "Pedido realizado exitosamente."
        };
        // Registrar el estado del pedido en la base de datos
        var estadoPedido = _IEstadoPedidoBussines.Create(estadoPedidoRequest);
        if (estadoPedido == null)
        {
          return StatusCode(500, "Error al crear el estado del pedido para el detalle de la venta con ID " + detalle.IdDetalleVentas);
        }
        // Obtener los tokens de notificación de los usuarios
        var deviceTokens = await _IUsuarioBussnies.GetNotificationTokensAsync(); // Enviar notificación de nuevo pedido a cada token

        foreach (var token in deviceTokens)
        {
          await _IOrderMesageFirebase.SendFirebaseNotificationAsync(token, "Nuevo Pedido", "¡Tienes un nuevo pedido en la app!");
        }

      }

      // Devolver respuesta exitosa
      return Ok(new { Message = "Estado del pedido registrado correctamente para todos los detalles de la venta." });
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
    }
  }
}
