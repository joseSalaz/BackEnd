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
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }


        public List<Proveedor> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    public async Task<List<Proveedor>> getProveedorCategoria(int idCategoria, int? idSubcategoria)
    {
      var query = dbSet.Where(p =>
       p.Libros.Any(l =>
           l.IdSubcategoriaNavigation.IdCategoria
           == idCategoria));

      // filtro opcional
      if (idSubcategoria.HasValue)
      {
        query = query.Where(p =>
            p.Libros.Any(l =>
                l.IdSubcategoria == idSubcategoria.Value));
      }

      return await query
          .GroupBy(p => p.IdProveedor)
          .Select(g => g.First())
          .ToListAsync();
    }
    }
}
