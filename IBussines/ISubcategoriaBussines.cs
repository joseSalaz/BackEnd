<<<<<<< HEAD
using DBModel.DB;
using Models.RequestResponse;
using Models.RequestResponse.libro;
=======
﻿using DBModel.DB;
using Models.RequestResponse;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
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
<<<<<<< HEAD
      Task<List<LibroCatalogo>> GetLibrosCatalogoBySubcategoria(int idSubcategoria);

  }
=======
  
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
