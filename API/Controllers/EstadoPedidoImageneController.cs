using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPedidoImageneController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IEstadoPedidoImageneBussines _IEstadoPedidoImageneBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public EstadoPedidoImageneController(IEstadoPedidoImageneBussines estadoPedidoImageneBussines, IMapper mapper)
        {
            _IEstadoPedidoImageneBussines = estadoPedidoImageneBussines;
            _Mapper = mapper;
        }
        #endregion

        #region crud methods
        /// <summary>
        /// Retorna todos los registros
        /// </summary>
        /// <returns>Retorna todos los registros</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<EstadoPedidoImageneResponse> lsl = _IEstadoPedidoImageneBussines.getAll();
            return Ok(lsl);
        }

        /// <summary>
        /// retorna el registro por Primary key
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns>retorna el registro</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstadoPedidoImageneResponse res = _IEstadoPedidoImageneBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] EstadoPedidoImageneRequest request)
        {
            EstadoPedidoImageneResponse res = _IEstadoPedidoImageneBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] EstadoPedidoImageneRequest request)
        {
            EstadoPedidoImageneResponse res = _IEstadoPedidoImageneBussines.Update(request);
            return Ok(res);
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="id">Valor del PK</param>
        /// <returns>Cantidad de registros afectados</returns>
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int res = _IEstadoPedidoImageneBussines.Delete(id);
            return Ok(res);
        }
        #endregion

        [HttpPost("create-with-images")]
        public async Task<IActionResult> CreateWithImages([FromForm] EstadoPedidoImageneRequest request, [FromForm] List<IFormFile> images)
        {
            if (request.IdEstadoPedido == null || !images.Any())
                return BadRequest("No se recibieron imágenes para subir.");

            try
            {
                var pedidoRequest = new EstadoPedidoImageneRequest { IdEstadoPedido = request.IdEstadoPedido };
                var responses = await _IEstadoPedidoImageneBussines.CreateWithImagesAsync(pedidoRequest, images);
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al procesar la solicitud: {ex.Message}");
            }
        }

    }
}
