using DBModel.DB;
using IRepositorio;
using IRepository;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Models.RequestResponse.libro;
=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
  {
<<<<<<< HEAD
    public CategoriaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
    public List<Categoria> GetAutoComplete(string query)
    {
      throw new NotImplementedException();
    }
<<<<<<< HEAD
=======
    public CategoriaRepository() : base() { }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4

    public async Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId)
    {
      return await Task.Run(() =>
      {
        return dbSet.Where(c => c.Subcategoria.Any(sc => sc.IdCategoria == categoriaId))
                  .SelectMany(c => c.Subcategoria)
                  .ToList();
      });
    }

<<<<<<< HEAD
    public async Task<List<LibroCatalogo>> GetLibrosByCategoriaId(int categoriaId)
    {
      return await dbSet.AsNoTracking()
                        .Where(c => c.IdCategoria == categoriaId)
                        .SelectMany(c => c.Subcategoria)
                        .SelectMany(sc => sc.Libros)
                        .Select(l => new LibroCatalogo
                        {
                          Libro = l, // Aquí pasamos TODA la tabla libro
                          Precio = l.Precios.Select(p => p.PrecioVenta).FirstOrDefault() ?? 0m
                        })
=======
    public async Task<List<Libro>> GetLibrosByCategoriaId(int categoriaId)
    {
      return await dbSet.Where(c => c.IdCategoria == categoriaId)
                        .SelectMany(c => c.Subcategoria)
                        .SelectMany(sc => sc.Libros)
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
                        .ToListAsync();
    }
  }
}
