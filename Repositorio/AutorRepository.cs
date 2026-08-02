using DBModel.DB;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Generic;

namespace Repository
{
  public class AutorRepository : GenericRepository<Autor>, IAutorRepository
  {
<<<<<<< HEAD
        public AutorRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
    public List<Autor> GetAutoComplete(string query)
    {
      throw new NotImplementedException();
    }

    public async Task<Autor> GetByIds(List<int> ids)
    {
      return await dbSet.Where(autor => ids.Contains(autor.IdAutor)).FirstOrDefaultAsync();
    }
    public async Task<Autor> GetByName(string nombre)
    {
      return await dbSet.FirstOrDefaultAsync(autor => autor.Nombre.ToLower() == nombre.ToLower());
    }

    public async Task<Autor> GetByIdAsync(object id)
    {
      return await dbSet.FindAsync(id);
    }


    public async Task<List<Autor>> GetAutorCategoria(int idCategoria, int? idSubcategoria)
    {
      var query = dbSet
    .Where(a => a.LibroAutors.Any(la =>
        la.IdLibroNavigation.IdSubcategoriaNavigation.IdCategoria
        == idCategoria));

      if (idSubcategoria.HasValue)
      {
        query = query.Where(a =>
            a.LibroAutors.Any(la =>
                la.IdLibroNavigation.IdSubcategoria
                == idSubcategoria.Value));
      }

      return await query
          .GroupBy(a => a.IdAutor)
          .Select(g => g.First())
          .ToListAsync();
    }
  }
}
