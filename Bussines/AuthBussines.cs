using AutoMapper;
using Bussnies;
using Constantes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using IBussines;
using IBussnies;
using Models.RequestResponse;
using Models.ResponseResponse;
using UtilSecurity.UtilSecurity;

namespace Bussines
{
    public class AuthBussnies : IAuthBussines
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioBussnies _userBussnies;
        private readonly appSettings _appSettings;
        public AuthBussnies(IMapper mapper, appSettings appSettings)
        {
            _userBussnies = new UsuarioBussnies(mapper);
            _mapper = mapper;
            _appSettings = appSettings;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public LoginResponse login(LoginRequest request)
        {
            LoginResponse res = new LoginResponse();
            UsuarioResponse user = _userBussnies.GetByUserName(request.email);
            if (user.Username != null && !(user.Username.ToLower() == request.email.ToLower()))
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

        //public async Task<LoginResponse> LoginWithGoogle(LoginRequest request)
        //{
        //    try
        //    {
        //        // Autenticar con Firebase utilizando el token de ID de Google
        //        var firebaseToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.IdToken);

        //        // Construye la respuesta con el token de Firebase
        //        var loginResponse = new LoginResponse
        //        {
        //            Success = true,
        //            Token = firebaseToken.Uid, // O cualquier otra propiedad que contenga el identificador único del usuario en Firebase
        //                                       // Otros campos de respuesta según sea necesario
        //        };

        //        return loginResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de errores
        //        Console.WriteLine("Error en la autenticación con Google y Firebase: " + ex.Message);
        //        return new LoginResponse { Success = false, Message = "Error en la autenticación con Google y Firebase: " + ex.Message };
        //    }
        //}
    }
}
