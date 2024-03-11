using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class DetalleDocEntradaRequest
    {
        public int IdDetalleDocEntrada { get; set; }

        public decimal? PrecioCosto { get; set; }

        public int? Cantidad { get; set; }

        public int IdDocEntrada { get; set; }

        public decimal? PorcentajeUtil { get; set; }

        public int? MinStock { get; set; }

        public int IdLibro { get; set; }
    }
}
