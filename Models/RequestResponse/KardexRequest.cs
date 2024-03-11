using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class KardexRequest
    {
        public int IdSucursal { get; set; }

        public int IdLibro { get; set; }

        public int? CantidadSalida { get; set; }

        public int? CantidadEntrada { get; set; }

        public int? Stock { get; set; }

        public decimal? UltPrecioCosto { get; set; }
    }
}
