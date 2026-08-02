using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;
using Models.RequestResponse;
namespace IBussines
{
    public interface IFavoritoBussines: ICRUDBussnies<FavoritoRequest, FavoritoResponse>
    {
        Task<List<FavoritoResponse>> ObtenerFavoritosPorUsuarioAsync(int idPersona);
        Task<bool> VerificarEsFavoritoAsync(int idPersona, int idLibro);
        Task<int> DeleteByPersonaAndLibroAsync(int idPersona, int idLibro);
    }
}
