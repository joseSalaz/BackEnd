using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class ClienteRequest
    {
        public int IdCliente { get; set; }

        public int? CantidadLibrosComprados { get; set; }

        public decimal? MontoConsumido { get; set; }

        public int IdPersonasR { get; set; }

    }
}
