using DBModel.DB;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Models.RequestResponse;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DetalleVentaRepository : GenericRepository<DetalleVenta>, IDetalleVentaRepository
    {


        public List<DetalleVenta> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentasByPersonaId(int idPersona)
        {
            var detalleVentas = await db.DetalleVentas
                                        .Include(dv => dv.IdVentasNavigation) 
                                        .Where(dv => dv.IdVentasNavigation.IdPersona == idPersona)
                                        .ToListAsync();
            return detalleVentas;
        }

        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentasByVentaId(int idVenta)
        {
            var detalleVentas = await db.DetalleVentas
                                        .Include(dv => dv.IdVentasNavigation) // Relación con la tabla Venta
                                        .Where(dv => dv.IdVentas == idVenta)   // Filtra por el id de la Venta
                                        .ToListAsync();
            return detalleVentas;
        }


        public async Task<bool> UpdateEstadoPedidosByVentaId(int idVenta, EstadoPedidoRequest request)
        {
            // Obtener los ids de detalle venta asociados al idVenta
            var detalleVentas = await db.DetalleVentas
                                        .Where(dv => dv.IdVentas == idVenta)
                                        .Select(dv => dv.IdDetalleVentas)
                                        .ToListAsync();

            // Si no hay detalles de venta, no hacemos nada
            if (!detalleVentas.Any())
                return false;

            // Filtrar los registros de EstadoPedido relacionados con los detalles de venta
            var estadosPedidos = await db.EstadoPedidos
                                        .Where(ep => detalleVentas.Contains(ep.IdDetalleVentas))
                                        .ToListAsync();

            // Actualizar los campos para cada registro de EstadoPedido
            foreach (var estadoPedido in estadosPedidos)
            {
                estadoPedido.Estado = request.Estado;
                estadoPedido.FechaEstado = request.FechaEstado;
                estadoPedido.Comentario = request.Comentario;
            }

            // Guardar los cambios
            await db.SaveChangesAsync();
            return true;
        }





    }
}
