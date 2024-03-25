using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using IRepositorio;
using Microsoft.EntityFrameworkCore;
using Repository.Generic;

namespace Repository
{
    public class LibroRepository : GenericRepository<Libro>, ILibroRepository
    {
        public List<Libro> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Libro>> GetByIds(List<int> ids)
        {
            return await dbSet.Where(libro => ids.Contains(libro.IdLibro)).ToListAsync();
        }

        public async Task<Libro> GetLibroConPreciosYPublicoObjetivo(int libroId)
        {
            return await dbSet.Where(l => l.IdLibro == libroId)
                .Include(l => l.Precios)
                .ThenInclude(p => p.IdPublicoObjetivoNavigation)
                .FirstOrDefaultAsync();
        }
<<<<<<< HEAD
        public async Task<List<Precio>> GetPreciosByLibroId(int libroId)
=======


        public async Task<Precio> GetPrecioByLibroId(int libroId)
>>>>>>> 0fa52fca5beecd5bc8ff6fc622c0844e62e69ea6
        {
            var libro = await dbSet
        .Include(l => l.Precios)
        .FirstOrDefaultAsync(l => l.IdLibro == libroId);

            return libro.Precios.ToList();

        }

    }
}  

