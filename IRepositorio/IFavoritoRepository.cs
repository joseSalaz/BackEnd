using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;
namespace IRepository
{
    public interface IFavoritoRepository: ICRUDRepositorio<Favorito>
    {
        Task<List<Favorito>> GetFavoritosByUsuarioAsync(int idPersona);
        Task<bool> EsFavoritoAsync(int idPersona, int idLibro);
        Task<int> DeleteByPersonaAndLibroAsync(int idPersona, int idLibro);
    }
}
