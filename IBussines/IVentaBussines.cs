<<<<<<< HEAD
using DBModel.DB;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using Models.RequestResponse;
using UtilInterface;

namespace IBussines
{
<<<<<<< HEAD
  public interface IVentaBussines : ICRUDBussnies<VentaRequest, VentaResponse>
  {
    Task<List<DetalleVenta>> GetDetalleVentaByVentaId(int idVenta);
    Task<MemoryStream> CreateVentaPdf(int idVenta);
    Task GenerarYEnviarPdfDeVenta(int idVenta, string emailCliente);
    Task<string> GenerarNumeroComprobante();
    Task<IEnumerable<VentaRequest>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
    Task<(List<VentaResponse>, int)> GetVentaPaginados(int page, int pageSize, string estado, bool ordenarPorFechaDesc, DateTime? fechaInicio, DateTime? fechaFin);
    Task<(VentaResponse venta, List<DetalleVentaResponse> detalles, EstadoPedidoResponse estado)> GetVentaConDetallesYEstado(int idVenta);
    Task<bool> AsignarDireccionAVenta(int idVenta, int idDireccion);
    bool ExisteVentaConDireccion(int idDireccion);
    Task<List<DetalleVenta>> ObtenerDetallesPorIdVenta(int idVenta);
    Task<List<Venta>> ObtenerVentasPorIdPersona(int idPersona);
    Task<EstadoPedido> ObtenerEstadoPedidoUnicoPorVenta(int idDetalleVenta);
    Task<VentaDetalledireccionResponse> GetVentaConPersonaYDireccion(int idVenta);
    Task<List<IngresoMensualResponse>> ObtenerIngresosMensuales(DateTime fechaInicio, DateTime fechaFin);
    Task<List<(Venta venta, List<DetalleVenta> detalles, EstadoPedido estado)>> ObtenerVentasPorMes(int anio, int mes);
    Task<byte[]> GenerarReporteVentasExcel(int anio, int mes);

    /// <summary>
    /// Registra una venta completa (persona, venta, actualización de caja y de stock/kardex,
    /// y detalle de venta) como UNA sola transacción: si cualquier paso falla (p. ej. stock
    /// insuficiente en el tercer ítem del carrito), se revierte todo lo anterior (rollback),
    /// incluyendo lo que ya se había guardado en caja o kardex dentro de la misma operación.
    /// </summary>
    Task<VentaCompletaResponse> RegistrarVentaConDetalleAsync(DatalleCarrito detalleCarrito);

    /// <summary>
    /// Confirma un pago online: crea venta, actualiza caja/stock, detalle y estado del pedido
    /// dentro de una única transacción. Email y notificaciones push deben ejecutarse fuera,
    /// después de que este método retorne con éxito.
    /// </summary>
    Task<VentaCompletaResponse> ConfirmarPagoYActualizarStockAsync(DatalleCarrito carrito, ConfirmarPagoOptions options);
  }
=======
    public interface IVentaBussines:ICRUDBussnies<VentaRequest, VentaResponse>
    {
        Task<List<DetalleVenta>> GetDetalleVentaByVentaId(int idVenta);
        Task<MemoryStream> CreateVentaPdf(int idVenta);
        Task GenerarYEnviarPdfDeVenta(int idVenta, string emailCliente);
        Task<string> GenerarNumeroComprobante();
        Task<IEnumerable<VentaRequest>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<(List<VentaResponse>, int)> GetVentaPaginados(int page, int pageSize, string estado, bool ordenarPorFechaDesc, DateTime? fechaInicio, DateTime? fechaFin);
        Task<(VentaResponse venta, List<DetalleVentaResponse> detalles, EstadoPedidoResponse estado)> GetVentaConDetallesYEstado(int idVenta);
        Task<bool> AsignarDireccionAVenta(int idVenta, int idDireccion);
        bool ExisteVentaConDireccion(int idDireccion);
        Task<List<DetalleVenta>> ObtenerDetallesPorIdVenta(int idVenta);
        Task<List<Venta>> ObtenerVentasPorIdPersona(int idPersona);
        Task<EstadoPedido> ObtenerEstadoPedidoUnicoPorVenta(int idDetalleVenta);
        Task<VentaDetalledireccionResponse> GetVentaConPersonaYDireccion(int idVenta);
        Task<List<IngresoMensualResponse>> ObtenerIngresosMensuales(DateTime fechaInicio, DateTime fechaFin);
        Task<List<(Venta venta, List<DetalleVenta> detalles, EstadoPedido estado)>> ObtenerVentasPorMes(int anio, int mes);
        Task<byte[]> GenerarReporteVentasExcel(int anio, int mes);
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
