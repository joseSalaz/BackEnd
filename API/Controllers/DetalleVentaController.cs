using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    //[Authorize]
    public class DetalleVentaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IDetalleVentaBussines _IDetalleVentaBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public DetalleVentaController(IMapper mapper)
        {
            _Mapper = mapper;
            _IDetalleVentaBussines = new DetalleVentaBussines(_Mapper);
        }
        #endregion

        #region crud methods
        /// <summary>
        /// Retorna todos los registros
        /// </summary>
        /// <returns>Retorna todos los registros</returns>
        [HttpGet("lista")]
        public IActionResult GetAll()
        {
            List<DetalleVentaResponse> lsl = _IDetalleVentaBussines.getAll();
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
            DetalleVentaResponse res = _IDetalleVentaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost("Guardar")]
        public IActionResult Create([FromBody] DetalleVentaRequest request)
        {
            DetalleVentaResponse res = _IDetalleVentaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut("Actualizar")]
        public IActionResult Update([FromBody] DetalleVentaRequest request)
        {
            DetalleVentaResponse res = _IDetalleVentaBussines.Update(request);
            return Ok(res);
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="id">Valor del PK</param>
        /// <returns>Cantidad de registros afectados</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult delete(int id)
        {
            int res = _IDetalleVentaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
