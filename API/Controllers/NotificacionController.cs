using Google.Apis.Auth.OAuth2.Requests;
using IBussnies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly IUsuarioBussnies _IUsuarioBussnies;
        public NotificacionController(IUsuarioBussnies usuarioBussnies)
        {
            _IUsuarioBussnies = usuarioBussnies;
        }


        [HttpPost("register-token")]
        public IActionResult RegisterToken([FromBody] TokensRequest request)
        {
            var result = _IUsuarioBussnies.RegisterNotificationToken(request.UsuarioId, request.Token); if (!result) { return NotFound(new { Message = "Usuario no encontrado." }); }
            return Ok(new { Message = "Token registrado correctamente" });
        }
    }
}

