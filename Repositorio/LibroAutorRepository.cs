<<<<<<< HEAD
using DBModel.DB;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
<<<<<<< HEAD
  public class LibroAutorRepository : GenericRepository<LibroAutor>, ILibroAutorRepository
  {
    public LibroAutorRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }


    public List<LibroAutor> GetAutoComplete(string query)
    {
      throw new NotImplementedException();
    }

    public async Task<List<Libro>> GetLibrosByAutorId(int autorId)
    {
      return await Task.Run(() =>
      {
        return dbSet.Where(la => la.IdAutor == autorId)
                          .Select(la => la.IdLibroNavigation)
                          .ToList();
      });
    }

    public async Task<List<Autor>> GetAutoresByLibroId(int libroId)
    {
      return await Task.Run(() =>
      {
        return dbSet.Where(la => la.IdLibro == libroId)
                          .Select(la => la.IdAutorNavigation)
                          .ToList();
        //like --->contains similitud 
      });
    }

  }
=======
    public class LibroAutorRepository : GenericRepository<LibroAutor>, ILibroAutorRepository
    {
        public LibroAutorRepository() : base() { }

        public List<LibroAutor> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Libro>> GetLibrosByAutorId(int autorId)
        {
            return await Task.Run(() =>
            {
                return dbSet.Where(la => la.IdAutor == autorId)
                            .Select(la => la.IdLibroNavigation)
                            .ToList();
            });
        }

        public async Task<List<Autor>> GetAutoresByLibroId(int libroId)
        {
            return await Task.Run(() =>
            {
                return dbSet.Where(la => la.IdLibro == libroId)
                            .Select(la => la.IdAutorNavigation)
                            .ToList();
        //like --->contains similitud 
            });
        }
        
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
