<<<<<<< HEAD
using DBModel.DB;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
using IRepository;
using Microsoft.EntityFrameworkCore;
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
<<<<<<< HEAD
        public EstadoPedidoRepository(DBModel.DB.LibreriaSaberContext context) : base(context) { }

=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
        public List<EstadoPedido> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<EstadoPedido> GetEstadoPedidoByDetalleVentaIdAsync(int idDetalleVenta)
        {
            var estadoPedido = await db.EstadoPedidos
                                        .Where(ep => ep.IdDetalleVentas == idDetalleVenta)  // Filtra por el id de detalleVenta
                                        .FirstOrDefaultAsync();  // Devuelve el primer resultado o null si no lo encuentra
            return estadoPedido;
        }
    }
}
