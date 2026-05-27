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
    public class TipoDocEntradaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ITipoDocEntradaBussines _ITipoDocEntradaBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public TipoDocEntradaController(IMapper mapper)
        {
            _Mapper = mapper;
            _ITipoDocEntradaBussines = new TipoDocEntradaBussines(_Mapper);
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
            List<TipoDocEntradaResponse> lsl = _ITipoDocEntradaBussines.getAll();
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
            TipoDocEntradaResponse res = _ITipoDocEntradaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] TipoDocEntradaRequest request)
        {
            TipoDocEntradaResponse res = _ITipoDocEntradaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] TipoDocEntradaRequest request)
        {
            TipoDocEntradaResponse res = _ITipoDocEntradaBussines.Update(request);
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
            int res = _ITipoDocEntradaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
