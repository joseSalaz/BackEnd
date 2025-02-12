using AutoMapper;
using Bussines;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        #region Variables
        private readonly IMapper _Mapper;
        private readonly IDireccionBussines _IDireccionBussines;
        private readonly IVentaBussines _IVentaBussines;
        #endregion

        #region Constructor
        public DireccionController(IDireccionBussines direccionBussines, IMapper mapper, IVentaBussines ventaBussines)
        {
            _Mapper = mapper;
            _IDireccionBussines = direccionBussines;
            _IVentaBussines = ventaBussines;
        }
        #endregion

        #region Métodos CRUD

        /// <summary>
        /// Retorna todas las direcciones del usuario
        /// </summary>
        [HttpGet("usuario/{idUsuario}")]
        public async Task<IActionResult> GetDireccionesByUsuario(int idUsuario)
        {
            var direcciones = await _IDireccionBussines.GetDireccionesPorUsuario(idUsuario);
            return Ok(direcciones);
        }


        /// <summary>
        /// Retorna una dirección por su ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = _IDireccionBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta una nueva dirección
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DireccionRequest request)
        {
            var direccionesUsuario = await _IDireccionBussines.GetDireccionesPorUsuario(request.IdPersona);
            if (!direccionesUsuario.Any())
            {
                request.EsPredeterminada = true;
            }

            var res = _IDireccionBussines.Create(request);
            return Ok(res);
        }


        /// <summary>
        /// Actualiza una dirección (solo si no está vinculada a ventas)
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody] DireccionRequest request)
        {
            var existeVenta = _IVentaBussines.ExisteVentaConDireccion(request.IdDireccion);
            if (existeVenta)
            {
                return BadRequest("No puedes actualizar una dirección que ya ha sido usada en una venta.");
            }

            var res = _IDireccionBussines.Update(request);
            return Ok(res);
        }

        /// <summary>
        /// Establece una dirección como predeterminada
        /// </summary>
        [HttpPut("set-predeterminada/{idDireccion}")]
        public async Task<IActionResult> SetDireccionPredeterminada(int idDireccion)
        {
            var resultado = await _IDireccionBussines.SetDireccionPredeterminada(idDireccion);
            if (!resultado)
            {
                return BadRequest("Error al actualizar la dirección predeterminada.");
            }
            return Ok("Dirección predeterminada actualizada correctamente.");
        }


        /// <summary>
        /// "Elimina" una dirección (pero solo la oculta si ya está usada en ventas)
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existeVenta = _IVentaBussines.ExisteVentaConDireccion(id);
            if (existeVenta)
            {
                return BadRequest("No puedes eliminar esta dirección porque ya fue usada en una venta. Puedes desactivarla.");
            }

            int res = _IDireccionBussines.Delete(id);
            return Ok(res);
        }

        #endregion
    }
}
