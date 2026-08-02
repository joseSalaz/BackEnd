using DBModel.DB;
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DatosGeneralesRepository : GenericRepository<DatosGenerale>, IDatosGeneralesRepository
    {
        public DatosGeneralesRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

        public List<DatosGenerale> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
