using DBModel.DB;
using IRepository;
using Microsoft.EntityFrameworkCore;
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

    }
}
