
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
    public class EstadoPedidoImageneRepository : GenericRepository<EstadoPedidoImagene>, IEstadoPedidoImageneRepository
    {
        public EstadoPedidoImageneRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

        public List<EstadoPedidoImagene> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }
    }
}
