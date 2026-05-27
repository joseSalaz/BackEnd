using DBModel.DB;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface ICategoriaBussines: ICRUDBussnies<CategoriaRequest, CategoriaResponse>
    {
        Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId);
        Task<List<Libro>> GetLibrosByCategoriaId(int categoriaId);
    }
}
