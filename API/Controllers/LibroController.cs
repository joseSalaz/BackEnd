using AutoMapper;
using Bussines;
//using Bussines;
using IBussines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        #endregion

        #region constructor 
        public LibroController(IMapper mapper)
        {
            _Mapper = mapper;
            _ILibroBussines = new LibroBussines(_Mapper);
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
        public async Task<IActionResult> Create([FromForm] LibroRequest request)
        {
            if (request.Imagen == null || request.Imagen.Length == 0)
            {
                return BadRequest("La imagen es requerida.");
            }

            try
            {
                
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Imagen.FileName)}";

                
                string filePath = Path.Combine("Imagenes", fileName);

                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Imagen.CopyToAsync(stream);
                }

                request.RutaImagen = filePath;

                
                LibroResponse res = _ILibroBussines.Create(request);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }



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
        #endregion
    }
}
