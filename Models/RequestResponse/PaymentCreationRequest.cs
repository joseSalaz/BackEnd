using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class PaymentCreationRequest
    {
        public decimal Amount { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
