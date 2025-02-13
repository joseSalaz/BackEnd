using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
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
    public class SubcategoriaRepository : GenericRepository<Subcategoria>, ISubcategoriaRepository
    {
        public SubcategoriaRepository() : base() { }

        public List<Subcategoria> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> GetLibroIdsBySubcategoria(int subcategoriaId)
        {
            var libroIds = await dbSet
                .Where(subcategoria => subcategoria.Id == subcategoriaId)
                .SelectMany(subcategoria => subcategoria.Libros.Select(libro => libro.IdLibro))
                .ToListAsync();
            return libroIds;
        }


        public async Task<(List<Subcategoria>, int)> FiltrarSubcategoriasAsync(int? categoriaId, int page, int pageSize)
        {
            var query = dbSet.AsQueryable();

            // Filtrar por categoría si se proporciona
            if (categoriaId.HasValue)
            {
                query = query.Where(s => s.IdCategoria == categoriaId.Value);
            }

            int totalItems = await query.CountAsync();
            var subcategorias = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (subcategorias, totalItems);
        }


    }


}

