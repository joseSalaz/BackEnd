
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
    public class SucursalRepository : GenericRepository<Sucursal>, ISucursalRepository
    {

        public SucursalRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }


        public List<Sucursal> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
