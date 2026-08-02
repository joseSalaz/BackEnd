using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comon
{
    public class AzureCognitiveServicesSettings
    {
        public string Endpoint { get; set; }
        public string SubscriptionKey { get; set; }
    }
    public class ValidarImagenRequest
    {
        public IFormFile File { get; set; }
    }

}
