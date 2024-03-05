using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comon
{
    public class TokenClaims
    {
        public int UserId { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Role { get; set; } = "";
        public string RoleName { get; set; } = "";
    }
}
