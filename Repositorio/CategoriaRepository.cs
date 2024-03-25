using DBModel.DB;
using IRepositorio;
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
    public class CategoriaRepository: GenericRepository<Categoria>, ICategoriaRepository
    {
        public List<Categoria> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
        public CategoriaRepository() : base() { }

        public async Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId)
        {
            return await Task.Run(() =>
            {
                return dbSet.Where(c => c.Subcategoria.Any(sc => sc.IdCategoria == categoriaId))
                    .SelectMany(c => c.Subcategoria)
                    .ToList();
            });
        }

        public async Task<List<Libro>> GetLibrosByCategoriaId(int categoriaId)
        {
            return await dbSet.Where(c => c.IdCategoria == categoriaId)
                              .SelectMany(c => c.Subcategoria)
                              .SelectMany(sc => sc.Libros)
                              .ToListAsync();
        }
    }
}
