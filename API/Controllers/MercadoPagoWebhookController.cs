using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MercadoPagoWebhookController : ControllerBase
  {
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook([FromBody] JsonElement body)
    {
      return Ok();
    }
  }
}
