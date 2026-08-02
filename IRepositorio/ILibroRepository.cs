using DBModel.DB;
using DBModel.Response;
using Models.RequestResponse;
<<<<<<< HEAD
using Models.RequestResponse.libro;
=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using UtilInterface;
using static Models.RequestResponse.FiltroLibro;

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
    Task<IEnumerable<Libro>> GetLibrosByVentaIdAsync(int idVenta);
    Task<List<LibroDataResponse>> getLibroAutor();
    Task<List<LibroFiltroResponse>> FiltrarLibros(FiltroLibroRequest request);
<<<<<<< HEAD
    Task<IEnumerable<LibroCatalogoDTO>> ObtenerCatalogoOptimizadoAsync();
    Task<List<Libro>> GetLibrosConPreciosByIds(List<int> ids);
=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4

  }
}
