using AutoMapper;
using Bussines;
using DBModel.DB;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    //[Authorize]
    public class PrecioController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IPrecioBussines _IPrecioBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public PrecioController(IMapper mapper)
        {
            _Mapper = mapper;
            _IPrecioBussines = new PrecioBussines(_Mapper);
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
            List<PrecioResponse> lsl = _IPrecioBussines.getAll();
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
            PrecioResponse res = _IPrecioBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] PrecioRequest request)
        {
            PrecioResponse res = _IPrecioBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] PrecioRequest request)
        {
            PrecioResponse res = _IPrecioBussines.Update(request);
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
            int res = _IPrecioBussines.Delete(id);
            return Ok(res);
        }

        [HttpGet("{precioId}/libro")]
        public async Task<ActionResult<Libro>> ObtenerLibroPorPrecioId(int precioId)
        {
            var libro = await _IPrecioBussines.ObtenerLibroPorPrecioId(precioId);
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        #endregion

    }
}
