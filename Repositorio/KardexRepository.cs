using DBModel.DB;
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
    public class KardexRepository : GenericRepository<Kardex>, IKardexRepository
    {
        public List<Kardex> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
        public async Task<Kardex> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(k => k.IdLibro == id);
        }
    }
}
