using AutoMapper;
using Bussines;
using DBModel.DB;

//using Bussines;
using IBussines;
using IService;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using Service;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class LibroController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly ILibroBussines _ILibroBussines = null;
        public readonly IMapper _Mapper;
        private readonly IAzureStorage _azureStorage;

        private readonly IConfiguration _configuration;
        #endregion

        #region constructor 
        public LibroController(IMapper mapper, ILibroBussines libroBussines, IAzureStorage azureStorage)
        {
            _Mapper = mapper;
            _ILibroBussines = libroBussines;
            _azureStorage = azureStorage;
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
            List<LibroResponse> lsl = _ILibroBussines.getAll();
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
            LibroResponse res = _ILibroBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LibroRequest request, IFormFile imageFile)
        {

            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Verificar si se proporcionó una imagen y guardarla en Azure Blob Storage
                    var imageUrl = await _ILibroBussines.CreateWithImage(request, imageFile);
                    return Ok(imageUrl);
                }
                else
                {
                    // Si no se proporcionó una imagen, crear el libro sin ella
                    var libroResponse = _ILibroBussines.Create(request);
                    return Ok(libroResponse);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un mensaje de error
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



        //[HttpPost]
        //public IActionResult Create([FromBody] LibroRequest request)
        //{
        //    LibroResponse res = _ILibroBussines.Create(request);
        //    return Ok(res);
        //}
        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] LibroRequest request)
        {
            LibroResponse res = _ILibroBussines.Update(request);
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
            int res = _ILibroBussines.Delete(id);
            return Ok(res);
        }

        [HttpGet("{libroId}/precios-objetivos")]
        public async Task<ActionResult<Libro>> ObtenerLibroConPreciosYPublicoObjetivo(int libroId)
        {
            // Obtener el libro con sus precios y el público objetivo asociado, devolviendo solo los IDs.
            var libroConIds = await _ILibroBussines.ObtenerLibroConPreciosYPublicoObjetivo(libroId);

            if (libroConIds == null)
            {
                return NotFound();
            }

            var libroCompleto = await _ILibroBussines.ObtenerLibroCompletoPorIds(libroConIds);

            if (libroCompleto == null)
            {
                return NotFound();
            }

            return Ok(libroCompleto);
        }


        [HttpGet("precios/{libroId}")]
        public async Task<IActionResult> GetPreciosByLibroId(int libroId)
        {
            // Obtener precios del servicio de negocios
            var precios = await _ILibroBussines.GetPreciosByLibroId(libroId);

            // Verificar si hay precios y devolver la lista de precios o una lista vacía
            if (precios != null)
            {
                return Ok(precios);
            }
            else
            {
                return Ok(new List<Precio>()); // Devolver una lista vacía en lugar de un código de error
            }
        }

        [HttpGet("stock/{libroId}")]
        public async Task<IActionResult> GetStockByLibroId(int libroId)
        {
            var stock = await _ILibroBussines.GetStockByLibroId(libroId);
            if (stock != null)
            {
                return Ok(stock);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
    #endregion


