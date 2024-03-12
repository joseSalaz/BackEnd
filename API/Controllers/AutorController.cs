using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Bussines;
using Models.RequestResponse;

namespace API.Controllers
{

    [Route("[controller]")]
    [ApiController]

    //[Authorize]
    public class AutorController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IAutorBussines _IAutorBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public AutorController(IMapper mapper)
        {
            _Mapper = mapper;
            _IAutorBussines = new AutorBussines(_Mapper);
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
            List<AutorResponse> lsl = _IAutorBussines.getAll();
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
            AutorResponse res = _IAutorBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] AutorRequest request)
        {
            AutorResponse res = _IAutorBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] AutorRequest request)
        {
            AutorResponse res = _IAutorBussines.Update(request);
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
            int res = _IAutorBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
