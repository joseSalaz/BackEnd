using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPedidoController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IEstadoPedidoBussines _IEstadoPedidoBussines = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public EstadoPedidoController(IMapper mapper)
        {
            _Mapper = mapper;
            _IEstadoPedidoBussines = new EstadoPedidoBussines(_Mapper);
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
            List<EstadoPedidoResponse> lsl = _IEstadoPedidoBussines.getAll();
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
            EstadoPedidoResponse res = _IEstadoPedidoBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] EstadoPedidoRequest request)
        {
            EstadoPedidoResponse res = _IEstadoPedidoBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] EstadoPedidoRequest request)
        {
            EstadoPedidoResponse res = _IEstadoPedidoBussines.Update(request);
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
            int res = _IEstadoPedidoBussines.Delete(id);
            return Ok(res);
        }
        #endregion
    }
}
