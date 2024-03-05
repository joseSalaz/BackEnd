using DBModel.DB;
using IRepositorio;
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoriaRepository: GenericRepository<Categoria>, ICategoriaRepository
    {
        public List<Categoria> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
