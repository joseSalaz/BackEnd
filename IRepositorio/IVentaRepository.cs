using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface IVentaRepository : ICRUDRepositorio<Venta>
    {
        Task<List<DetalleVenta>> GetDetallesByVentaId(int idVenta);
        Task<(Venta venta, List<DetalleVenta> detalles)> GetVentaConDetalles(int idVenta);
        Task<Persona> GetPersonaByVentaId(int idVenta);
        Task<string> ObtenerUltimoNumeroComprobante();
        Task<IEnumerable<Venta>> ObtenerVentasPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<(List<Venta>, int)> GetVentaPaginados(int page, int pageSize, string estado = null, bool ordenarPorFechaDesc = true, DateTime? fechaInicio = null, DateTime? fechaFin = null);
        Task<(Venta venta, List<DetalleVenta> detalles, EstadoPedido estado)> GetVentaConDetallesYEstado(int idVenta);
        Task<int> SaveChangesAsync();
        bool ExisteVentaConDireccion(int idDireccion);
        void AsignarDireccionAVenta(Venta venta, int idDireccion);
    }
}
