
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
using System.Threading.Tasks;

namespace Repository
{
    public class FavoritoRepository : GenericRepository<Favorito>, IFavoritoRepository
    {
        public FavoritoRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

        public async Task<int> DeleteByPersonaAndLibroAsync(int idPersona, int idLibro)
        {
            // Usamos tu contexto (dbSet o _context según lo tengas definido en tu GenericRepository)
            var favorito = await dbSet
                .FirstOrDefaultAsync(f => f.IdPersona == idPersona && f.IdLibro == idLibro);

            if (favorito != null)
            {
                dbSet.Remove(favorito);
                return await db.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<bool> EsFavoritoAsync(int idPersona, int idLibro)
        {
            return await dbSet.AnyAsync(f => f.IdPersona == idPersona && f.IdLibro == idLibro);
        }

        public List<Favorito> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Favorito>> GetFavoritosByUsuarioAsync(int idUsuario)
        {
            return await dbSet
                .Include(f => f.IdLibroNavigation)
                .Where(f => f.IdPersona == idUsuario)
                .OrderByDescending(f => f.FechaAgregado)
                .ToListAsync();
        }
    }
}
