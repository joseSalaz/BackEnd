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
    public class PrecioRepository : GenericRepository<Precio>, IPrecioRepository
    {
        public PrecioRepository() : base()
        {
        }
        public List<Precio> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }


        public async Task<Libro> GetLibroByPrecioId(int precioId)
        {
            return await dbSet.Where(p => p.IdPrecios == precioId)
            .Select(p => p.IdLibroNavigation)
            .FirstOrDefaultAsync();
        }

    }
}
