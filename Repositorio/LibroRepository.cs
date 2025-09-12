using Constantes;
using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using IRepositorio;
using Microsoft.EntityFrameworkCore;
using Models.Comon;
using Repository.Generic;
using System.Net;

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

        public async Task<Libro> GetByIdAsync(object id)
        {
            return await dbSet.Where(libro => libro.IdLibro == (int)id).FirstOrDefaultAsync();
        }

        public async Task<Libro> GetLibroConPreciosYPublicoObjetivo(int libroId)
        {
            return await dbSet.Where(l => l.IdLibro == libroId)
                .Include(l => l.Precios)
                .ThenInclude(p => p.IdPublicoObjetivoNavigation)
                .FirstOrDefaultAsync();
        }
        
        public async Task<List<Precio>> GetPreciosByLibroId(int libroId)

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

        public async Task<Kardex> GetKardexByLibroId(int libroId)
        {
            var libro = await dbSet
                .Include(l => l.Kardex)
                .FirstOrDefaultAsync(l => l.IdLibro == libroId);

            return libro?.Kardex;
        }

        public async Task<(List<Libro>, int)> GetLibrosPaginados(int page, int pageSize)
        {
            var query = dbSet.AsQueryable();
            int totalItems = await query.CountAsync();
            var libros = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (libros, totalItems);
        }


        public async Task<List<Libro>> filtroComplete(string query)
        {
            return await dbSet
                .Where(libro => EF.Functions.Like(libro.Titulo, $"%{query}%"))
                .ToListAsync();
        }

        public async Task<bool> CambiarEstadoLibro(int libroId)
        {
            var libro = await dbSet.FindAsync(libroId);
            if (libro == null)
            {
                return false; // Retornar false si el libro no existe
            }

            libro.Estado = false; // Alternar estado
            await db.SaveChangesAsync();

            return true; // Retornar true si se actualizó correctamente
        }

        public async Task<(List<Libro>, int)> FiltrarLibrosAsync(bool? estado, string titulo, int page, int pageSize)
        {
            var query = dbSet.AsQueryable();

            // Filtrar por estado si se proporciona
            if (estado.HasValue)
            {
                query = query.Where(l => l.Estado == estado.Value);
            }

            // Filtrar por título si se proporciona
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                query = query.Where(l => EF.Functions.Like(l.Titulo, $"%{titulo}%"));
            }

            int totalItems = await query.CountAsync();
            var libros = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (libros, totalItems);
        }

        public async Task<IEnumerable<Libro>> GetLibrosByVentaIdAsync(int idVenta)
        {
            var libros = await db.Libros
                                 .Join(db.DetalleVentas,
                                       l => l.IdLibro,
                                       dv => dv.IdLibro,
                                       (l, dv) => new { Libro = l, DetalleVenta = dv })
                                 .Where(joined => joined.DetalleVenta.IdVentas == idVenta)
                                 .Select(joined => joined.Libro)
                                 .ToListAsync();

            return libros;
        }

    }
}  

