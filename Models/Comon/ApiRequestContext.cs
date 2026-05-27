using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comon
{
    public class ApiRequestContext
    {
        public string Ip { get; set; } = "";
        public string AbsoluteUri { get; set; } = "";
        public string AbsolutePath { get; set; } = "";
        public string Host { get; set; } = "";
        public string Method { get; set; } = "";
        public string UserAgent { get; set; } = "";
        public string Controller { get; set; } = "";
        public string BodyRequest { get; set; } = "";
    }
}
