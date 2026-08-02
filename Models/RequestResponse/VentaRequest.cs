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

        public decimal? TotalPrecio { get; set; }

        public string? TipoComprobante { get; set; }

        public DateTime? FechaVenta { get; set; }

        public string? NroComprobante { get; set; }

        public int IdPersona { get; set; }

        public int IdUsuario { get; set; }
        public int IdCaja { get; set; }
        public int IdDireccion { get; set; }
    }
}
