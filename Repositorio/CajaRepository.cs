using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
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
  public class CajaRepository : GenericRepository<Caja>, ICajaRepository
  {
<<<<<<< HEAD
        public CajaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
    public List<Caja> GetAutoComplete(string query)
    {
      throw new NotImplementedException();
    }



    // Método para obtener todas las Cajas con fecha de hoy
    public List<Caja> GetCajasDeHoy()
    {
      var today = DateTime.Today;
      var cajasDeHoy = dbSet.Where(c => c.Fecha.HasValue && c.Fecha.Value.Date == today).ToList();
      return cajasDeHoy;
    }


    // Método para buscar una caja por la fecha actual
    public Caja FindCajaByDate(DateTime date)
    {
      return dbSet.FirstOrDefault(c => c.Fecha.HasValue && c.Fecha.Value.Date == date.Date);
    }

    public Caja GetCajaEcommerce()
    {
<<<<<<< HEAD
      return dbSet.FirstOrDefault(c => c.IdCaja == 4);
    }
=======
      return dbSet.FirstOrDefault(c => c.IdCaja == 1013);
    }





>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
  }
}
