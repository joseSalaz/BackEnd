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
    public class LibroAutorController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ILibroAutorBussines _ILibroAutorBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public LibroAutorController(IMapper mapper)
        {
            _Mapper = mapper;
            _ILibroAutorBussines = new LibroAutorBussines(_Mapper);
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
            List<LibroAutorResponse> lsl = _ILibroAutorBussines.getAll();
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
        //    LibroAutorResponse res = _ILibroAutorBussines.getById(id);
        //    return Ok(res);
        //}

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] LibroAutorRequest request)
        {
            LibroAutorResponse res = _ILibroAutorBussines.Create(request);
            return Ok(res);
        }

        ///// <summary>
        ///// Actualiza un registro
        ///// </summary>
        ///// <param name="entity">registro a actualizar</param>
        ///// <returns>retorna el registro Actualiza</returns>
        //[HttpPut("Actualizar")]
        //public IActionResult Update([FromBody] LibroAutorRequest request)
        //{
        //    LibroAutorResponse res = _ILibroAutorBussines.Update(request);
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
        //    int res = _ILibroAutorBussines.Delete(id);
        //    return Ok(res);
        //}
        #endregion

        [HttpGet("GetLibrosByAutorId/{autorId}")]
        public async Task<IActionResult> GetLibrosByAutorId(int autorId)
        {
            var libros = await _ILibroAutorBussines.GetLibrosByAutorId(autorId);
            return Ok(libros);
        }

        /// <summary>
        /// Obtiene los autores asociados a un libro por su ID.
        /// </summary>
        /// <param name="libroId">ID del libro.</param>
        [HttpGet("GetAutoresByLibroId/{libroId}")]
        public async Task<IActionResult> GetAutoresByLibroId(int libroId)
        {
            var autores = await _ILibroAutorBussines.GetAutoresByLibroId(libroId);
            return Ok(autores);
        }
    }
}
