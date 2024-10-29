using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LoginRequest
    {
        public string username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
