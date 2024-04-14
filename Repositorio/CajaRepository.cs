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


       


    }
}
