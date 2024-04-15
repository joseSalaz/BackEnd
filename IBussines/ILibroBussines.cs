using DBModel.DB;
using IService;
using Microsoft.AspNetCore.Http;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;


namespace IBussines
{
    public interface ILibroBussines : ICRUDBussnies<LibroRequest,LibroResponse>
    {
        Task<LibroResponse> CreateWithImage(LibroRequest entity, IFormFile imageFile);
        Task<List<Libro>> GetLibrosByIds(List<int> ids);
        Task<Libro> ObtenerLibroConPreciosYPublicoObjetivo(int libroId);
        Task<Libro> ObtenerLibroCompletoPorIds(Libro libroConIds);
        Task<List<Precio>> GetPreciosByLibroId(int libroId);
        Task<Kardex> GetKardexByLibroId(int libroId);
        Task<(List<LibroResponse>, int)> GetLibrosPaginados(int page, int pageSize);
        Task<List<LibroResponse>> filtroComplete(string query);
    }
}
