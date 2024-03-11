using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibroGeneroController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ILibroGeneroBussines _ILibroGeneroBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public LibroGeneroController(IMapper mapper)
        {
            _Mapper = mapper;
            _ILibroGeneroBussines = new LibroGeneroBussines(_Mapper);
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
            List<LibroGeneroResponse> lsl = _ILibroGeneroBussines.getAll();
            return Ok(lsl);
        }

        ///// <summary>
        ///// retorna el registro por Primary key
        ///// </summary>
        ///// <param name="id">PK</param>
        ///// <returns>retorna el registro</returns>
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    LibroGeneroResponse res = _ILibroGeneroBussines.getById(id);
        //    return Ok(res);
        //}

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost("Guardar")]
        public IActionResult Create([FromBody] LibroGeneroRequest request)
        {
            LibroGeneroResponse res = _ILibroGeneroBussines.Create(request);
            return Ok(res);
        }

        ///// <summary>
        ///// Actualiza un registro
        ///// </summary>
        ///// <param name="entity">registro a actualizar</param>
        ///// <returns>retorna el registro Actualiza</returns>
        //[HttpPut("Actualizar")]
        //public IActionResult Update([FromBody] LibroGeneroRequest request)
        //{
        //    LibroGeneroResponse res = _ILibroGeneroBussines.Update(request);
        //    return Ok(res);
        //}

        ///// <summary>
        ///// Elimina un registro
        ///// </summary>
        ///// <param name="id">Valor del PK</param>
        ///// <returns>Cantidad de registros afectados</returns>
        //[HttpDelete("Eliminar/{id}")]
        //public IActionResult delete(int id)
        //{
        //    int res = _ILibroGeneroBussines.Delete(id);
        //    return Ok(res);
        //}
        #endregion
    }
}
