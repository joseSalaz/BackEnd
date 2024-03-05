using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class ApisPeruPersonaResponse
    {
        public bool success { get; set; }
        public string dni { get; set; } = "";
        public string nombres { get; set; } = "";
        public string apellidoPaterno { get; set; } = "";
        public string apellidoMaterno { get; set; } = "";
        public string codVerifica { get; set; } = "";
    }
}
