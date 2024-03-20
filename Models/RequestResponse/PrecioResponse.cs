using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class PrecioResponse
    {
        public int IdPrecios { get; set; }

        public decimal? PrecioVenta { get; set; }

        public decimal? PorcUtilidad { get; set; }

        public int IdLibro { get; set; }

        public int? IdPublicoObjetivo { get; set; }
    }
}
