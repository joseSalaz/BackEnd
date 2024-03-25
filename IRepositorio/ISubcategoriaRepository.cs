using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface ISubcategoriaRepository : ICRUDRepositorio<Subcategoria>
    {
        Task<List<int>> GetLibroIdsBySubcategoria(int subcategoriaId);
    }
}
