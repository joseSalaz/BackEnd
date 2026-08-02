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
    public class DocEntradaRepository : GenericRepository<DocEntrada>, IDocEntradaRepository
    {
        public DocEntradaRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

        public List<DocEntrada> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
