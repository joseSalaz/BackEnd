using DBModel.DB;
using Models.RequestResponse;
using UtilInterface;

namespace IBussines
{
    public interface IVentaBussines:ICRUDBussnies<VentaRequest, VentaResponse>
    {
        Task<List<DetalleVenta>> GetDetalleVentaByVentaId(int idVenta);
        Task<MemoryStream> CreateVentaPdf(int idVenta);
        Task GenerarYEnviarPdfDeVenta(int idVenta, string emailCliente);
        Task<string> GenerarNumeroComprobante();
        Task<IEnumerable<VentaRequest>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<(List<VentaResponse>, int)> GetVentaPaginados(int page, int pageSize, string estado, bool ordenarPorFechaDesc, DateTime? fechaInicio, DateTime? fechaFin);
        Task<(VentaResponse venta, List<DetalleVentaResponse> detalles, EstadoPedidoResponse estado)> GetVentaConDetallesYEstado(int idVenta);
    }
}
