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
    public class VentaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IVentaBussines _IVentaBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public VentaController(IMapper mapper)
        {
            _Mapper = mapper;
            _IVentaBussines = new VentaBussines(_Mapper);
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
            List<VentaResponse> lsl = _IVentaBussines.getAll();
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
            VentaResponse res = _IVentaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost("Guardar")]
        public IActionResult Create([FromBody] VentaRequest request)
        {
            VentaResponse res = _IVentaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut("Actualizar")]
        public IActionResult Update([FromBody] VentaRequest request)
        {
            VentaResponse res = _IVentaBussines.Update(request);
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
            int res = _IVentaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
