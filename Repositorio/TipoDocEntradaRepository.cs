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
    public class TipoDocEntradaRepository : GenericRepository<TipoDocEntrada>, ITipoDocEntradaRepository
    {
        public TipoDocEntradaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

        public List<TipoDocEntrada> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
