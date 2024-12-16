using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class EstadoPedidoResponse
    {
        public int IdEstadoPedido { get; set; }

        public int IdDetalleVentas { get; set; }

        public string Estado { get; set; } = null!;

        public DateTime FechaEstado { get; set; }

        public string? Comentario { get; set; }
    }
}
