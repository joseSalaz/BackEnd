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
    public class CategoriaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ICategoriaBussines _ICategoriaBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public CategoriaController(IMapper mapper)
        {
            _Mapper = mapper;
            _ICategoriaBussines = new CategoriaBussines(_Mapper);
        }
        #endregion

        #region crud methods
        /// <summary>
        /// Retorna todos los registros
        /// </summary>
        /// <returns>Retorna todos los registros</returns>
        [HttpGet ]
        public IActionResult GetAll()
        {
            List<CategoriaResponse> lsl = _ICategoriaBussines.getAll();
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
            CategoriaResponse res = _ICategoriaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] CategoriaRequest request)
        {
            CategoriaResponse res = _ICategoriaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut ]
        public IActionResult Update([FromBody] CategoriaRequest request)
        {
            CategoriaResponse res = _ICategoriaBussines.Update(request);
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
            int res = _ICategoriaBussines.Delete(id);
            return Ok(res);
        }
        #endregion
        [HttpGet("{id}/subcategorias")]
        public async Task<IActionResult> GetSubcategorias(int id)
        {
            var subcategorias = await _ICategoriaBussines.GetSubcategoriasByCategoriaId(id);
            return Ok(subcategorias);
        }

        [HttpGet("{categoriaId}/libros")]
        public async Task<IActionResult> GetLibrosByCategoriaId(int categoriaId)
        {
            var libros = await _ICategoriaBussines.GetLibrosByCategoriaId(categoriaId);
            return Ok(libros);
        }


    }
}
