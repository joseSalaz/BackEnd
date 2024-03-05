using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comon
{
    public class InfoRequest
    {
        public TokenClaims Claims { get; set; } = new TokenClaims();
        public ApiRequestContext RequestHttp { get; set; } = new ApiRequestContext();
    }
}
