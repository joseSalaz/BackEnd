using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface IPrecioRepository : ICRUDRepositorio<Precio>
    {
        Task<Libro> GetLibroByPrecioId(int precioId);
        Task<Precio> GetByIdAsync(int id);
    }
}
