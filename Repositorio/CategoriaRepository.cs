using DBModel.DB;
using IRepositorio;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Models.RequestResponse.libro;
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
    public CategoriaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

    public List<Categoria> GetAutoComplete(string query)
    {
      throw new NotImplementedException();
    }

    public async Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId)
    {
      return await Task.Run(() =>
      {
        return dbSet.Where(c => c.Subcategoria.Any(sc => sc.IdCategoria == categoriaId))
                  .SelectMany(c => c.Subcategoria)
                  .ToList();
      });
    }

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
                        .ToListAsync();
    }
  }
}
