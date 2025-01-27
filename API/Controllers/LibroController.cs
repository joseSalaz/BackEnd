using AutoMapper;
using DBModel.DB;
using IBussines;
using IService;
using Microsoft.AspNetCore.Mvc;
using Models.Comon;
using Models.RequestResponse;


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
        private readonly IAzureComputerVisionService _visionService;

        private readonly IConfiguration _configuration;
        #endregion

        #region constructor 
        public LibroController(IMapper mapper, ILibroBussines libroBussines, IAzureComputerVisionService visionService)
        {
            _Mapper = mapper;
            _ILibroBussines = libroBussines;
            _visionService = visionService;

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
        //[HttpPost]
        //public async Task<IActionResult> Create([FromForm] LibroRequest request, IFormFile imageFile = null)
        //{
        //    try
        //    {
        //        if (imageFile != null && imageFile.Length > 0)
        //        {
        //            var imageUrl = await _ILibroBussines.CreateWithImage(request, imageFile);
        //            return Ok(imageUrl);
        //        }
        //        else
        //        {
        //            var libroResponse = _ILibroBussines.Create(request);
        //            return Ok(libroResponse);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Error interno del servidor: " + ex.Message);
        //    }
        //}




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
        /// <returns>retorna el registro actualizado</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] LibroconautorRequest request, IFormFile? imageFile, decimal precioVenta, int stock)
        {
            // Esperamos a que la tarea se complete
            LibroResponse res = await _ILibroBussines.UpdateLib(request, imageFile, precioVenta, stock);
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

        [HttpGet("kardex/{libroId}")]
        public async Task<IActionResult> GetKardexByLibroId(int libroId)
        {
            var kardex = await _ILibroBussines.GetKardexByLibroId(libroId);
            if (kardex != null)
            {
                return Ok(kardex);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Paginator")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                var (libroResponses, totalItems) = await _ILibroBussines.GetLibrosPaginados(page, pageSize);
                var response = new
                {
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    Libros = libroResponses
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("filtroComplete")]
        public async Task<IActionResult> Autocomplete(string titulo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(titulo))
                {
                    return BadRequest("La consulta de búsqueda no puede estar vacía.");
                }

                var results = await _ILibroBussines.filtroComplete(titulo);
                // En lugar de retornar un NotFound si no hay resultados, simplemente retorna una lista vacía
                return Ok(results ?? new List<LibroResponse>());
            }
            catch (Exception ex)
            {
                // Es una buena práctica manejar excepciones inesperadas para evitar que la app se caiga y dar una respuesta al cliente.
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



        /// <summary>
        /// Inserta un nuevo registro con la opción de subir un archivo a Firebase
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <param name="imageFile">Archivo de imagen a subir</param>
        /// <returns>Retorna el registro insertado con la URL de la imagen</returns>
        [HttpPost("create-with-image-firebase")]
        public async Task<IActionResult> CreateWithImageFirebase([FromForm] LibroconautorRequest request, decimal precioVenta, int stock, IFormFile imageFile = null)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Llama al método en la capa de negocio que maneja la creación y subida de imagen a Firebase
                    var libroResponse = await _ILibroBussines.CreateWithImageFirebase(request, imageFile, precioVenta, stock);
                    return Ok(libroResponse);
                }
                else
                {
                    var libroResponse = _ILibroBussines.Create(request.Libro);
                    return Ok(libroResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        /// <summary>
        /// Sube una imagen y devuelve los detalles proporcionados por Azure Computer Vision.
        /// </summary>
        /// <param name="request">Archivo de imagen</param>
        /// <returns>Detalles de la imagen analizada</returns>
        [HttpPost("detalles-imagen")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ObtenerDetallesImagen([FromForm] ValidarImagenRequest request)
        {
            // Validar que el archivo fue enviado
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No se subió ningún archivo.");

            // Leer el archivo como stream
            using var stream = request.File.OpenReadStream();

            // Llamar al servicio para obtener los detalles de la imagen
            var analysisResult = await _visionService.AnalyzeImageAsync(stream);

            // Retornar los detalles obtenidos
            return Ok(analysisResult);
        }
        #endregion

    }
}
    


