using DBModel.DB;
using UtilInterface;

namespace IRepositorio
{
    public interface ILibroRepository : ICRUDRepositorio<Libro>
    {
        public Task<List<Libro>> GetByIds(List<int> ids);
        Task<Libro> GetLibroConPreciosYPublicoObjetivo(int libroId);
        Task<List<Precio>> GetPreciosByLibroId(int libroId);
        Task<int?> GetStockByLibroId(int libroId);
    }
}