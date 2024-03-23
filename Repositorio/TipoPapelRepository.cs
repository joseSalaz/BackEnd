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
    public class TipoPapelRepository : GenericRepository<TipoPapel>, ITipoPapelRepository
    {
        public List<TipoPapel> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
