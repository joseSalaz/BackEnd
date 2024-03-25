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
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return await dbSet.Where(l => l.IdLibro == libroId)
                      .Select(l => new Libro
                      {
                          IdLibro = l.IdLibro,
                          Precios = l.Precios.Select(p => new Precio
                          {
                              IdPrecios = p.IdPrecios, 
                              IdPublicoObjetivo = p.IdPublicoObjetivo
                          }).ToList()
                      })
                      .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }

    
}