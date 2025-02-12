using DBModel.DB;
using UtilInterface;

namespace IRepositorio
{
    public interface ILibroRepository : ICRUDRepositorio<Libro>
    {
        public Task<List<Libro>> GetByIds(List<int> ids);
        Task<Libro> GetByIdAsync(object id);
        Task<Libro> GetLibroConPreciosYPublicoObjetivo(int libroId);
        Task<List<Precio>> GetPreciosByLibroId(int libroId);
        Task<Kardex> GetKardexByLibroId(int libroId);
        Task<(List<Libro>, int)> GetLibrosPaginados(int page, int pageSize);
        Task<List<Libro>> filtroComplete(string query);
        Task<bool> CambiarEstadoLibro(int libroId);
        Task<(List<Libro>, int)> FiltrarLibrosAsync(bool? estado, string titulo, int page, int pageSize);

    }
}