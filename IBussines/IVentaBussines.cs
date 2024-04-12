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
    }
}
