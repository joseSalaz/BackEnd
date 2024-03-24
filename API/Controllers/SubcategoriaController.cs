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
    public class SubcategoriaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ISubcategoriaBussines _ISubcategoriaBussines = null;
        public readonly IMapper _Mapper;
        private readonly ILibroBussines _IlibroBussines;
        #endregion

        #region constructor 
        public SubcategoriaController(IMapper mapper, ILibroBussines libroBussines)
        {
            _Mapper = mapper;
            _ISubcategoriaBussines = new SubcategoriaBussines(_Mapper);
            _IlibroBussines = libroBussines;
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
            List<SubcategoriaResponse> lsl = _ISubcategoriaBussines.getAll();
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
            SubcategoriaResponse res = _ISubcategoriaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] SubcategoriaRequest request)
        {
            SubcategoriaResponse res = _ISubcategoriaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] SubcategoriaRequest request)
        {
            SubcategoriaResponse res = _ISubcategoriaBussines.Update(request);
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
            int res = _ISubcategoriaBussines.Delete(id);
            return Ok(res);
        }
        [HttpGet("librosbysubcategoria/{subcategoriaId}")]
        public async Task<IActionResult> GetLibrosBySubcategoria(int subcategoriaId)
        {
          
            var libroIds = await _ISubcategoriaBussines.GetLibrosIdsBySubcategoria(subcategoriaId);


            var libros = await _IlibroBussines.GetLibrosByIds(libroIds);

            return Ok(libros);
        }

        #endregion
    }
}
