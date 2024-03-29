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

        public async Task<List<Precio>> GetPreciosByLibroId(int libroId)
>>>>>>> 345ca54308d54fa8f3926f90415e50a369469619
        {
            var libro = await dbSet
                .Include(l => l.Precios)
                .FirstOrDefaultAsync(l => l.IdLibro == libroId);

            // Verificar si el libro existe y tiene precios asociados
            if (libro != null && libro.Precios != null)
            {
                return libro.Precios.ToList();
            }

            // Si no hay precios asociados, devolver una lista vacía
            return new List<Precio>();
        }

        public async Task<int?> GetStockByLibroId(int libroId)
        {
            var libro = await dbSet
                .Include(l => l.Kardex)
                .FirstOrDefaultAsync(l => l.IdLibro == libroId);

            return libro?.Kardex?.Stock;
        }

    }
}  

