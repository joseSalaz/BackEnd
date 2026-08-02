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
  public class PrecioRepository : GenericRepository<Precio>, IPrecioRepository
  {
    public PrecioRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

    public List<Precio> GetAutoComplete(string query)
    {
      throw new NotImplementedException();
    }


    public async Task<Libro> GetLibroByPrecioId(int precioId)
    {
      return await dbSet.Where(p => p.IdPrecios == precioId)
      .Select(p => p.IdLibroNavigation)
      .FirstOrDefaultAsync();
    }

    public async Task<Precio> GetByIdAsync(int id)
    {
      return await dbSet.FirstOrDefaultAsync(p => p.IdLibro == id);
    }

  }
=======
    public class PrecioRepository : GenericRepository<Precio>, IPrecioRepository
    {
        public PrecioRepository() : base()
        {
        }
        public List<Precio> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }


        public async Task<Libro> GetLibroByPrecioId(int precioId)
        {
            return await dbSet.Where(p => p.IdPrecios == precioId)
            .Select(p => p.IdLibroNavigation)
            .FirstOrDefaultAsync();
        }

        public async Task<Precio> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.IdLibro == id);
        }

    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
