using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class EstadoPedidoImageneRequest
    {
        public int IdEstadoPedidoImagen { get; set; }

        public int IdEstadoPedido { get; set; }

        public string UrlImagen { get; set; }
        public string? Estado { get; set; }

        public DateTime? Fecha { get; set; }

    }
}

