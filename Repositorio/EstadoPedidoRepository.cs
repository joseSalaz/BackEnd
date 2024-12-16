using DBModel.DB;
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace Repository
{
    public class EstadoPedidoRepository : GenericRepository<EstadoPedido>, IEstadoPedidoRepository
    {
        public List<EstadoPedido> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
