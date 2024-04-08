using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class ExecutePaymentModelRequest
    {
        public string PaymentId { get; set; }
        public string PayerID { get; set; }

        public DatalleCarrito Carrito { get; set; }

        public MetodoDePago MetodoDePago { get; set; }
     }
}
