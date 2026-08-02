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
<<<<<<< HEAD
  public interface ICategoriaBussines : ICRUDBussnies<CategoriaRequest, CategoriaResponse>
  {
    Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId);
    Task<List<LibroCatalogo>> GetLibrosByCategoriaId(int categoriaId);
  }
=======
    public interface ICategoriaBussines: ICRUDBussnies<CategoriaRequest, CategoriaResponse>
    {
        Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId);
        Task<List<Libro>> GetLibrosByCategoriaId(int categoriaId);
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
