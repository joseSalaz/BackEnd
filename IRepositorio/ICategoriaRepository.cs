<<<<<<< HEAD
using DBModel.DB;
using Models.RequestResponse.libro;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
<<<<<<< HEAD
  public interface ICategoriaRepository : ICRUDRepositorio<Categoria>
  {
    Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId);

    Task<List<LibroCatalogo>> GetLibrosByCategoriaId(int categoriaId);
  }
=======
    public interface ICategoriaRepository: ICRUDRepositorio<Categoria>
    {
        Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId);

        Task<List<Libro>> GetLibrosByCategoriaId(int categoriaId);
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
