using AutoMapper;
using Bussines;
using Bussnies;
using IBussines;
using IBussnies;
using Models.RequestResponse;
using Models.ResponseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilSecurity.UtilSecurity;

namespace Bussines
{
    public class AuthBussnies : IAuthBussines
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioBussnies _userBussnies;
        public AuthBussnies(IMapper mapper)
        {
            _userBussnies = new UsuarioBussnies(mapper);
            _mapper = mapper;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public LoginResponse login(LoginRequest request)
        {
            LoginResponse res = new LoginResponse();
            UsuarioResponse user = _userBussnies.GetByUserName(request.Username);
            if (user.Username != null && !(user.Username.ToLower() == request.Username.ToLower()))
            {
                res.Message = "Usuario y/o password invalido";
                res.Usuario = null;
                return res;
            }
            string newPassword = UtilCripto.encriptar_AES(request.Password);
            if (!(newPassword == user.Password))
            {
                res.Message = "Usuario y/o password invalido";
                res.Usuario = null;
                return res;
            }
            res.Usuario = user;
            return res;
        }
    }
}
