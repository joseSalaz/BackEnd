using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class VentaRequest
    {
        public int IdVentas { get; set; }

        public int? IdLibroR { get; set; }

        public int? IdComprobanteR { get; set; }

        public decimal? TotalPrecio { get; set; }

        public int? Cantidad { get; set; }

        public DateTime? FechaVenta { get; set; }

        public string? TipoPago { get; set; }
    }
}
