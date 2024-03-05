using DBModel.DB;
using IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Generic;

namespace Repository
{
    public class AutorRepository : GenericRepository<Autor>, IAutorRepository
    {
        public List<Autor> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
