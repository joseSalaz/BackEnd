using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class ExecuteMercadopagoRequest
    {
        public string PaymentId { get; set; }
        public string PreferenceId { get; set; }

        public DatalleCarrito Carrito { get; set; }

        public MetodoDePago MetodoDePago { get; set; }
    }
}
