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
    public interface ISubcategoriaBussines : ICRUDBussnies<SubcategoriaRequest, SubcategoriaResponse>
    {
        Task<List<int>> GetLibrosIdsBySubcategoria(int subcategoriaId);
    }
}
