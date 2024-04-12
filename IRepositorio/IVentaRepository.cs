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
    }
}
