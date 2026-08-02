using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptoController : ControllerBase
    {
        private readonly ICriptoService _criptoService;

        public CriptoController(ICriptoService criptoService)
        {
            _criptoService = criptoService;
        }

        [HttpPost("desencriptar")]
        public IActionResult Desencriptar([FromBody] DesencriptarRequest request)
        {
            if (string.IsNullOrEmpty(request.EncryptedText))
            {
                return BadRequest("El texto encriptado es requerido.");
            }

            try
            {
                string decryptedText = _criptoService.Desencriptar(request.EncryptedText);
                return Ok(new { decryptedText });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al desencriptar: {ex.Message}");
            }
        }
    }
}
