using AutoMapper;
using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepositorio;
using IRepository;
using IService;
using Models.RequestResponse;
using Repository;
using UnitOfWork;


using UtilPDF;

namespace Bussines
{

  public class VentaBussines : IVentaBussines

  {
    #region Declaracion de vcariables generales
    public readonly IVentaRepository _IVentaRepository = null;
    public readonly IMapper _Mapper;
    public readonly IEmailService _emailService;
    public readonly IDireccionRepository _IDireccionRepository = null;

    #endregion

    private readonly IUnitOfWork _unitOfWork;

    #region constructor 
    public VentaBussines(IMapper mapper, IEmailService emailService, IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      _Mapper = mapper;
      _IVentaRepository = _unitOfWork.Ventas;
      _emailService = emailService;
    }
    #endregion

    public VentaResponse Create(VentaRequest entity)
    {
      Venta au = _Mapper.Map<Venta>(entity);
      au = _IVentaRepository.Create(au);
      VentaResponse res = _Mapper.Map<VentaResponse>(au);
      return res;
    }

    public List<VentaResponse> CreateMultiple(List<VentaRequest> request)
    {
      List<Venta> au = _Mapper.Map<List<Venta>>(request);
      au = _IVentaRepository.InsertMultiple(au);
      List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(au);
      return res;
    }

    public int Delete(object id)
    {
      return _IVentaRepository.Delete(id);
    }

    public int deleteMultipleItems(List<VentaRequest> request)
    {
      List<Venta> au = _Mapper.Map<List<Venta>>(request);
      int cantidad = _IVentaRepository.DeleteMultipleItems(au);
      return cantidad;
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
    }

    public List<VentaResponse> getAll()
    {
      List<Venta> lsl = _IVentaRepository.GetAll();
      List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(lsl);
      return res;
    }

    public List<VentaResponse> getAutoComplete(string query)
    {
      throw new NotImplementedException();
    }

    public VentaResponse getById(object id)
    {
      Venta au = _IVentaRepository.GetById(id);
      VentaResponse res = _Mapper.Map<VentaResponse>(au);
      return res;
    }

    public VentaResponse Update(VentaRequest entity)
    {
      Venta au = _Mapper.Map<Venta>(entity);
      au = _IVentaRepository.Update(au);
      VentaResponse res = _Mapper.Map<VentaResponse>(au);
      return res;
    }

    public List<VentaResponse> UpdateMultiple(List<VentaRequest> request)
    {
      List<Venta> au = _Mapper.Map<List<Venta>>(request);
      au = _IVentaRepository.UpdateMultiple(au);
      List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(au);
      return res;
    }

    public async Task<List<DetalleVenta>> GetDetalleVentaByVentaId(int idVenta)
    {
      return await _IVentaRepository.GetDetallesByVentaId(idVenta);
    }


    public async Task<MemoryStream> CreateVentaPdf(int idVenta)
    {
      // Obtener la venta y sus detalles de venta.
      var result = await _IVentaRepository.GetVentaConDetalles(idVenta);
      var venta = result.venta;
      var detallesVenta = result.detalles;

      if (venta == null || !detallesVenta.Any())
      {
        throw new Exception("No se encontraron datos para la venta.");
      }

      // Obtener la información de la persona asociada con la venta.
      var persona = await _IVentaRepository.GetPersonaByVentaId(idVenta);
      if (persona == null)
      {
        throw new Exception("No se pudo obtener información de la persona para la venta.");
      }

      // Convierte los detalles de venta a DetalleVentaRequest si es necesario.
      List<DetalleVentaRequest> detallesVentaRequest = detallesVenta
          .Select(dv => _Mapper.Map<DetalleVentaRequest>(dv))
          .ToList();

      // Genera el PDF con la información de la venta, los detalles y la persona.
      MemoryStream pdfStream = PdfGenerator.CreateDetalleVentaPdf(detallesVentaRequest, venta, persona);

      return pdfStream;
    }

    public async Task GenerarYEnviarPdfDeVenta(int idVenta, string emailCliente)
    {
      // Suponemos que ya tienes un método que genera el PDF
      MemoryStream pdfStream = await this.CreateVentaPdf(idVenta);

      // Asegúrate de que la posición del stream esté al inicio antes de enviar.
      pdfStream.Position = 0;

      // Aquí asumimos que tienes un método en IEmailService que soporta enviar un stream como adjunto
      await _emailService.SendEmailAsync(
          emailCliente,
          "Tu Boleta de Venta Electrónica",
          "Se adjunta la Bolte Electronica en PDF",
          pdfStream,
          $"BoletaVenta_{idVenta}.pdf"
      );
    }
    public async Task<string> GenerarNumeroComprobante()
    {
      string ultimoComprobante = await _IVentaRepository.ObtenerUltimoNumeroComprobante();

      int numeroActual = 0;
      if (ultimoComprobante != null)
      {
        int.TryParse(ultimoComprobante.Substring(3), out numeroActual);
      }
      numeroActual++;
      return $"BOL{numeroActual.ToString("D4")}";
    }
    public async Task<IEnumerable<VentaRequest>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
    {
      var ventas = await _IVentaRepository.ObtenerVentasPorFechaAsync(fechaInicio, fechaFin);

      return ventas.Select(v => new VentaRequest
      {
        IdVentas = v.IdVentas,
        TotalPrecio = v.TotalPrecio,
        TipoComprobante = v.TipoComprobante,
        FechaVenta = v.FechaVenta,
        NroComprobante = v.NroComprobante,
        IdPersona = v.IdPersona,
        IdUsuario = v.IdUsuario
      });
    }

    public async Task<(List<VentaResponse>, int)> GetVentaPaginados(int page, int pageSize, string estado = null, bool ordenarPorFechaDesc = true, DateTime? fechaInicio = null, DateTime? fechaFin = null)
    {
      var (ventas, totalItems) = await _IVentaRepository.GetVentaPaginados(page, pageSize, estado, ordenarPorFechaDesc, fechaInicio, fechaFin);
      var response = _Mapper.Map<List<VentaResponse>>(ventas);
      return (response, totalItems);
    }

    public async Task<(VentaResponse venta, List<DetalleVentaResponse> detalles, EstadoPedidoResponse estado)> GetVentaConDetallesYEstado(int idVenta)
    {
      var (venta, detalles, estadoPedido) = await _IVentaRepository.GetVentaConDetallesYEstado(idVenta);

      if (venta == null)
      {
        return (null, new List<DetalleVentaResponse>(), null);
      }

      var ventaResponse = _Mapper.Map<VentaResponse>(venta);
      var detallesResponse = _Mapper.Map<List<DetalleVentaResponse>>(detalles);
      var estadoResponse = estadoPedido != null ? _Mapper.Map<EstadoPedidoResponse>(estadoPedido) : null;

      return (ventaResponse, detallesResponse, estadoResponse);
    }

    public async Task<bool> AsignarDireccionAVenta(int idVenta, int idDireccion)
    {
      var venta = _IVentaRepository.GetById(idVenta);
      if (venta == null)
        throw new Exception("No se encontró la venta.");

      var direccion = _IDireccionRepository.GetById(idDireccion);
      if (direccion == null || direccion.IdPersona != venta.IdPersona)
        throw new Exception("La dirección no es válida para esta venta.");

      venta.IdDireccion = idDireccion;
      _IVentaRepository.Update(venta);

      // El SaveChanges debe ser manejado correctamente
      return await Task.FromResult(true);
    }
    public bool ExisteVentaConDireccion(int idDireccion)
    {
      return _IVentaRepository.ExisteVentaConDireccion(idDireccion);
    }
    public async Task<List<Venta>> ObtenerVentasPorIdPersona(int idPersona)
    {
      return await _IVentaRepository.ObtenerVentasPorIdPersona(idPersona);
    }

    public async Task<List<DetalleVenta>> ObtenerDetallesPorIdVenta(int idVenta)
    {
      return await _IVentaRepository.ObtenerDetallesPorIdVenta(idVenta);
    }

    public async Task<EstadoPedido> ObtenerEstadoPedidoUnicoPorVenta(int idDetalleVenta)
    {
      return await _IVentaRepository.ObtenerEstadoPedidoUnicoPorVenta(idDetalleVenta);
    }
    public async Task<VentaDetalledireccionResponse> GetVentaConPersonaYDireccion(int idVenta)
    {
      return await _IVentaRepository.GetVentaConPersonaYDireccion(idVenta);
    }
    public async Task<List<IngresoMensualResponse>> ObtenerIngresosMensuales(DateTime fechaInicio, DateTime fechaFin)
    {
      return await _IVentaRepository.ObtenerIngresosMensuales(fechaInicio, fechaFin);
    }



    public async Task<List<(Venta venta, List<DetalleVenta> detalles, EstadoPedido estado)>> ObtenerVentasPorMes(int anio, int mes)
    {
      return await _IVentaRepository.GetVentasConDetallesYEstadoPorMes(anio, mes);
    }

    public async Task<byte[]> GenerarReporteVentasExcel(int anio, int mes)
    {
      var ventas = await ObtenerVentasPorMes(anio, mes);

      if (!ventas.Any())
      {
        throw new Exception("No hay ventas registradas en el mes seleccionado.");
      }

      // Transformar los datos para el Excel
      var datosExcel = ventas.SelectMany(v => v.detalles.Select(d => new
      {
        IdVenta = v.venta.IdVentas,
        FechaVenta = v.venta.FechaVenta?.ToString("yyyy-MM-dd HH:mm:ss"),
        TipoComprobante = v.venta.TipoComprobante,
        NroComprobante = v.venta.NroComprobante,
        Cliente = v.venta.IdPersona,
        Usuario = v.venta.IdUsuario,
        Producto = d.NombreProducto,
        PrecioUnitario = d.PrecioUnit,
        Cantidad = d.Cantidad,
        Total = d.Importe,
        FechaEstado = v.estado?.FechaEstado.ToString("yyyy-MM-dd HH:mm:ss") ?? "Sin fecha",
        ComentarioEstado = v.estado?.Comentario ?? "Sin comentario"
      })).ToList();

      return UtilExel.GenerarExcel.CrearExcel(datosExcel, $"Ventas_{anio}_{mes}");
    }

    /// <summary>
    /// Orquesta el registro completo de una venta (buscar/crear persona, tomar la caja del día,
    /// crear la venta, actualizar caja, validar y descontar stock por cada ítem del carrito y
    /// crear el detalle de venta) dentro de UNA SOLA transacción de base de datos.
    ///
    /// Antes esta lógica vivía repartida en el Controller, llamando a varios repositorios
    /// directamente y sin transacción: si fallaba a mitad de camino (p. ej. stock insuficiente
    /// en el ítem 3 de 5), la caja y el kardex de los ítems 1 y 2 ya habían quedado guardados,
    /// dejando datos inconsistentes. Con ExecuteInTransactionAsync, cualquier excepción revierte
    /// TODO lo hecho dentro de este método (rollback real vía EF Core).
    /// </summary>
    public async Task<VentaCompletaResponse> RegistrarVentaConDetalleAsync(DatalleCarrito detalleCarrito)
    {
      return await _unitOfWork.ExecuteInTransactionAsync(async () =>
      {
        // 1) Buscar la persona por documento, o crearla si no existe.
        var persona = _unitOfWork.Personas.GetByDni(detalleCarrito.Persona.NumeroDocumento);
        if (persona == null)
        {
          persona = new Persona
          {
            Nombre = detalleCarrito.Persona.Nombre,
            ApellidoPaterno = detalleCarrito.Persona.ApellidoPaterno,
            ApellidoMaterno = detalleCarrito.Persona.ApellidoMaterno,
            Correo = detalleCarrito.Persona.Correo,
            TipoDocumento = detalleCarrito.Persona.TipoDocumento,
            NumeroDocumento = detalleCarrito.Persona.NumeroDocumento,
            Telefono = detalleCarrito.Persona.Telefono,
          };
          persona = _unitOfWork.Personas.Create(persona);
          if (persona == null)
          {
            throw new InvalidOperationException("Error al crear la persona.");
          }
        }

        // 2) Tomar la caja abierta del día. Si no hay, no se puede continuar.
        var cajaDelDia = _unitOfWork.Cajas.FindCajaByDate(DateTime.Today);
        if (cajaDelDia == null)
        {
          throw new InvalidOperationException("No hay una caja abierta para hoy. Por favor, crea una caja primero.");
        }

        decimal totalVenta = detalleCarrito.Items.Sum(item => item.PrecioVenta * item.Cantidad);

        // 3) Crear la venta.
        var venta = new Venta
        {
          FechaVenta = DateTime.Now,
          TipoComprobante = "Boleta",
          IdUsuario = 1, // TODO: reemplazar por el usuario autenticado en sesión.
          NroComprobante = "FAC00", // TODO: generar dinámicamente (ver GenerarNumeroComprobante).
          IdPersona = persona.IdPersona,
          IdCaja = cajaDelDia.IdCaja,
          TotalPrecio = totalVenta
        };
        venta = _unitOfWork.Ventas.Create(venta);
        if (venta == null)
        {
          throw new InvalidOperationException("Error al crear la venta.");
        }

        // 4) Actualizar los montos de la caja del día.
        cajaDelDia.IngresosACaja += totalVenta;
        cajaDelDia.SaldoFinal = cajaDelDia.SaldoInicial + cajaDelDia.IngresosACaja;
        _unitOfWork.Cajas.Update(cajaDelDia);

        // 5) Validar stock y descontarlo (kardex) por cada ítem, preparando el detalle de venta.
        var detallesAInsertar = new List<DetalleVenta>();
        foreach (var item in detalleCarrito.Items)
        {
          var kardexActual = _unitOfWork.Kardex.GetById(item.libro.IdLibro);
          if (kardexActual == null || kardexActual.Stock < item.Cantidad)
          {
            // Esta excepción dispara el rollback de TODO lo hecho arriba (persona, venta, caja
            // y los descuentos de kardex de los ítems anteriores del mismo carrito).
            throw new InvalidOperationException($"No hay suficiente stock para el libro con ID {item.libro.IdLibro}.");
          }

          kardexActual.Stock -= item.Cantidad;
          _unitOfWork.Kardex.Update(kardexActual);

          detallesAInsertar.Add(new DetalleVenta
          {
            IdVentas = venta.IdVentas,
            NombreProducto = item.libro.Titulo,
            PrecioUnit = item.PrecioVenta,
            IdLibro = item.libro.IdLibro,
            Cantidad = item.Cantidad,
            Importe = item.PrecioVenta * item.Cantidad,
            Estado = "Pendiente"
          });
        }

        // 6) Crear los detalles de venta.
        var detallesCreados = _unitOfWork.DetalleVentas.InsertMultiple(detallesAInsertar);
        if (detallesCreados == null || !detallesCreados.Any())
        {
          throw new InvalidOperationException("Error al crear el detalle de la venta.");
        }

        return new VentaCompletaResponse
        {
          Mensaje = "Venta y detalles registrados con éxito",
          Venta = _Mapper.Map<VentaResponse>(venta),
          Detalles = _Mapper.Map<List<DetalleVentaResponse>>(detallesCreados)
        };
      });
    }

    /// <summary>
    /// Orquesta la confirmación de un pago online (webhook / execute-payment) dentro de UNA transacción.
    /// Antes esta lógica vivía en MercadoPagoController y PaypalController, llamando a varios
    /// repositorios y Bussines sin transacción compartida (MercadoPago) o con transacción manual
    /// en el controller (PayPal). Ahora todo pasa por UnitOfWork.ExecuteInTransactionAsync.
    /// </summary>
    public async Task<VentaCompletaResponse> ConfirmarPagoYActualizarStockAsync(
        DatalleCarrito carrito,
        ConfirmarPagoOptions options)
    {
      return await _unitOfWork.ExecuteInTransactionAsync(async () =>
      {
        if (carrito?.Items == null || !carrito.Items.Any())
        {
          throw new InvalidOperationException("El carrito está vacío o es inválido.");
        }

        if (carrito.Persona == null || carrito.Persona.IdPersona <= 0)
        {
          throw new InvalidOperationException("La persona del carrito no es válida.");
        }

        var caja = options.IdCaja.HasValue
            ? _unitOfWork.Cajas.GetById(options.IdCaja.Value)
            : _unitOfWork.Cajas.GetCajaEcommerce();

        if (caja == null)
        {
          throw new InvalidOperationException("No existe la caja ecommerce.");
        }

        var numeroComprobante = await GenerarNumeroComprobante();

        var venta = new Venta
        {
          FechaVenta = DateTime.Now,
          TipoComprobante = "Boleta",
          IdUsuario = 1,
          NroComprobante = numeroComprobante,
          IdPersona = carrito.Persona.IdPersona,
          TotalPrecio = carrito.TotalAmount,
          IdCaja = caja.IdCaja,
          IdDireccion = carrito.IdDireccion
        };

        venta = _unitOfWork.Ventas.Create(venta);
        if (venta == null || venta.IdVentas <= 0)
        {
          throw new InvalidOperationException("Error al crear la venta.");
        }

        if (options.ActualizarSaldoCaja)
        {
          caja.SaldoFinal = (caja.SaldoFinal ?? 0) + carrito.TotalAmount;
          _unitOfWork.Cajas.Update(caja);
        }

        var detallesAInsertar = new List<DetalleVenta>();
        foreach (var item in carrito.Items)
        {
          var kardexActual = _unitOfWork.Kardex.GetById(item.libro.IdLibro);
          if (kardexActual == null || kardexActual.Stock < item.Cantidad)
          {
            throw new InvalidOperationException(
                $"No hay suficiente stock para el libro con ID {item.libro.IdLibro}.");
          }

          kardexActual.Stock -= item.Cantidad;
          _unitOfWork.Kardex.Update(kardexActual);

          detallesAInsertar.Add(new DetalleVenta
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

        var detallesCreados = _unitOfWork.DetalleVentas.InsertMultiple(detallesAInsertar);
        if (detallesCreados == null || !detallesCreados.Any())
        {
          throw new InvalidOperationException("Error al crear el detalle de la venta.");
        }

        foreach (var detalle in detallesCreados)
        {
          var estadoPedido = new EstadoPedido
          {
            IdDetalleVentas = detalle.IdDetalleVentas,
            Estado = options.EstadoPedidoInicial,
            FechaEstado = DateTime.Now,
            Comentario = "Pedido realizado exitosamente."
          };

          estadoPedido = _unitOfWork.EstadosPedido.Create(estadoPedido);
          if (estadoPedido == null)
          {
            throw new InvalidOperationException(
                $"Error al crear el estado del pedido para el detalle con ID {detalle.IdDetalleVentas}.");
          }
        }

        return new VentaCompletaResponse
        {
          Mensaje = "Venta, detalles y estado registrados con éxito.",
          Venta = _Mapper.Map<VentaResponse>(venta),
          Detalles = _Mapper.Map<List<DetalleVentaResponse>>(detallesCreados)
        };
      });
    }

  }
}
