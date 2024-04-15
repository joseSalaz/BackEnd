using AutoMapper;
using Bussines;
using DBModel.DB;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using System.Net;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class PersonaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IPersonaBussines _IPersonaBussines = null;
        public readonly IMapper _Mapper;
        private readonly IPersonaBussines _PersonaBussnies;

        #endregion

        #region constructor 
        public PersonaController(IMapper mapper)
        {
            _Mapper = mapper;
            _IPersonaBussines = new PersonaBussines(_Mapper);
            _PersonaBussnies = new PersonaBussines(_Mapper);
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
            List<PersonaResponse> lsl = _IPersonaBussines.getAll();
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
            PersonaResponse res = _IPersonaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] PersonaRequest request)
        {
            PersonaResponse res = _IPersonaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] PersonaRequest request)
        {
            PersonaResponse res = _IPersonaBussines.Update(request);
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
            int res = _IPersonaBussines.Delete(id);
            return Ok(res);
        }
        #endregion

        /// <summary>
        /// retorna los datos de una persona en base al DNI
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("dni/{TipoDocumento}/{NumeroDocumento}")]

        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(GenericResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(GenericResponse))]
        public IActionResult GetWithDni(string TipoDocumento, string NumeroDocumento)
        {
            Persona vpersona = new Persona();
            vpersona = _PersonaBussnies.GetByTipoNroDocumento(TipoDocumento, NumeroDocumento);
            return Ok(vpersona);

        }
        [HttpPost("verificar")]
        public IActionResult VerificarUsuario([FromBody] PersonaRequest persona)
        {
            var personaExistente = _IPersonaBussines.GetByIdSub(persona.Sub);

            if (personaExistente != null)
            {
                // El usuario ya existe, podrías devolver el usuario existente o un token de sesión.
                return Ok(personaExistente);
            }
            else
            {

                // El usuario no existe, procedemos a crearlo.
                PersonaResponse nuevaPersona = _IPersonaBussines.Create(persona);
                if (nuevaPersona != null)
                {
                    return Ok(nuevaPersona);
                }
                else
                {
                    // Hubo un problema al crear el usuario.
                    return StatusCode(500, "No se pudo crear el usuario");
                }
            }
        }


        [HttpGet("Paginator")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                var (personaResponse, totalItems) = await _IPersonaBussines.GetPersonaPaginados(page, pageSize);
                var response = new
                {
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    persona = personaResponse
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
