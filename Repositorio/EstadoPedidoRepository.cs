using DBModel.DB;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace Repository
{
    public class EstadoPedidoRepository : GenericRepository<EstadoPedido>, IEstadoPedidoRepository
    {
        public List<EstadoPedido> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<EstadoPedido> GetEstadoPedidoByDetalleVentaIdAsync(int idDetalleVenta)
        {
            var estadoPedido = await db.EstadoPedidos
                                        .Where(ep => ep.IdDetalleVentas == idDetalleVenta)  // Filtra por el id de detalleVenta
                                        .FirstOrDefaultAsync();  // Devuelve el primer resultado o null si no lo encuentra
            return estadoPedido;
        }
    }
}
