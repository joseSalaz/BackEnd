using Models.ResponseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestResponse
{
    public class LoginResponse
    {
        public bool Success { get; set; } = false;
        public UsuarioResponse Usuario { get; set; } = new UsuarioResponse();
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
