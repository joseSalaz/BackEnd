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
    public class DetalleDocEntradaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IDetalleDocEntradaBussines _IDetalleDocEntradaBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public DetalleDocEntradaController(IMapper mapper)
        {
            _Mapper = mapper;
            _IDetalleDocEntradaBussines = new DetalleDocEntradaBussines(_Mapper);
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
            List<DetalleDocEntradaResponse> lsl = _IDetalleDocEntradaBussines.getAll();
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
            DetalleDocEntradaResponse res = _IDetalleDocEntradaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] DetalleDocEntradaRequest request)
        {
            DetalleDocEntradaResponse res = _IDetalleDocEntradaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] DetalleDocEntradaRequest request)
        {
            DetalleDocEntradaResponse res = _IDetalleDocEntradaBussines.Update(request);
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
            int res = _IDetalleDocEntradaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
