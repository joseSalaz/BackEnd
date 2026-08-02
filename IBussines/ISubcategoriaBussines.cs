using DBModel.DB;
using Models.RequestResponse;
using Models.RequestResponse.libro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface ISubcategoriaBussines : ICRUDBussnies<SubcategoriaRequest, SubcategoriaResponse>
    {
        Task<List<int>> GetLibrosIdsBySubcategoria(int subcategoriaId);
        Task<(List<Subcategoria>, int)> FiltrarSubcategoriasAsync(int? categoriaId, int page, int pageSize);
      Task<List<LibroCatalogo>> GetLibrosCatalogoBySubcategoria(int idSubcategoria);

  }
}
