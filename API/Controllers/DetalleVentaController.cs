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
    public class DetalleVentaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        private readonly IDetalleVentaBussines _detalleVentaBussines;
        private readonly IMapper _Mapper;

        #endregion

        #region constructor 
        public DetalleVentaController(IDetalleVentaBussines detalleVentaBussines, IMapper mapper)
        {
            _detalleVentaBussines = detalleVentaBussines;
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
            List<DetalleVentaResponse> lsl = _detalleVentaBussines.getAll();
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
            DetalleVentaResponse res = _detalleVentaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] DetalleVentaRequest request)
        {
            DetalleVentaResponse res = _detalleVentaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] DetalleVentaRequest request)
        {
            DetalleVentaResponse res = _detalleVentaBussines.Update(request);
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
            int res = _detalleVentaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
