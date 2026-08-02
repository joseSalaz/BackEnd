
using DBModel.DB;
using Models.RequestResponse.libro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{

  public interface ICategoriaRepository : ICRUDRepositorio<Categoria>
  {
    Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId);

    Task<List<LibroCatalogo>> GetLibrosByCategoriaId(int categoriaId);
  }

}
