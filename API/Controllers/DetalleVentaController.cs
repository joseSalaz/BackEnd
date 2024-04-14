using AutoMapper;
using Bussines;
using DBModel.DB;
using DocumentFormat.OpenXml.Vml.Office;
using IBussines;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using Repository;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    //[Authorize]
    public class DetalleVentaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        private readonly IDetalleVentaBussines _detalleVentaBussines;
        private readonly IMapper _Mapper;
        private readonly IKardexRepository _kardexRepository;
        private readonly IKardexBussines _kardexBussines;
        private readonly IDetalleVentaBussines _IDetalleVentaBussines = null;
        private readonly IVentaBussines _IVentaBussines = null;
        private readonly IPersonaBussines _IPersonaBussines;
        private readonly ICajaBussines _ICajaBussines;
        private readonly ICajaRepository _ICajaRepository;
        #endregion

        #region constructor 
        public DetalleVentaController(IDetalleVentaBussines detalleVentaBussines, IMapper mapper, IKardexRepository kardexRepository, IKardexBussines kardexBussines, IVentaBussines ventaBussines, IPersonaBussines personaBussines, ICajaBussines cajaBussines, ICajaRepository iCajaRepository)
        {
            _detalleVentaBussines = detalleVentaBussines;
            _Mapper = mapper;
            _kardexRepository = kardexRepository;
            _kardexBussines = kardexBussines;
            _IDetalleVentaBussines = detalleVentaBussines;
            _IVentaBussines = ventaBussines;
            _IPersonaBussines = personaBussines;
            _ICajaBussines = cajaBussines;
            _ICajaRepository = iCajaRepository;
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
            List<DetalleVentaResponse> lsl = _detalleVentaBussines.getAll();
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
            DetalleVentaResponse res = _detalleVentaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] DetalleVentaRequest request)
        {
            DetalleVentaResponse res = _detalleVentaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] DetalleVentaRequest request)
        {
            DetalleVentaResponse res = _detalleVentaBussines.Update(request);
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
            int res = _detalleVentaBussines.Delete(id);
            return Ok(res);
        }

        [HttpGet("traer/{idPersona}")]
        public async Task<IActionResult> GetDetalleVentasByPersonaId(int idPersona)
        {
                var detalleVentas = await _detalleVentaBussines.GetDetalleVentasByPersonaId(idPersona);

                return Ok(detalleVentas);
        }




        [HttpPost("registrar-venta-detalle")]
        public async Task<IActionResult> RegistrarVentaYDetalle([FromBody] DatalleCarrito detalleCarrito)
        {
            // Verificar si la Persona con el documento proporcionado ya existe
            var personaExistente =  _IPersonaBussines.GetPersonaByDocumento(detalleCarrito.Persona.NumeroDocumento);
            int idPersona;  // Solo se declara, no se inicializa aquí.

            if (personaExistente == null)
            {
                // La persona no existe, entonces la creamos
                PersonaRequest nuevaPersona = new PersonaRequest
                {
                    Nombre = detalleCarrito.Persona.Nombre,
                    ApellidoPaterno = detalleCarrito.Persona.ApellidoPaterno,
                    ApellidoMaterno = detalleCarrito.Persona.ApellidoMaterno,
                    Correo = detalleCarrito.Persona.Correo,
                    TipoDocumento = detalleCarrito.Persona.TipoDocumento,
                    NumeroDocumento = detalleCarrito.Persona.NumeroDocumento,
                    Telefono = detalleCarrito.Persona.Telefono,
                };
                var personaCreada = _IPersonaBussines.Create(nuevaPersona);
                if (personaCreada == null)
                {
                    return StatusCode(500, "Error al crear la persona");
                }
                idPersona = personaCreada.IdPersona; // Usamos el ID asignado automáticamente después de crear el registro
            }
            else
            {
                idPersona = personaExistente.IdPersona;
            }
            // Verificar la existencia de una caja para el día actual
            var cajaDelDia = _ICajaBussines.RegistrarVentaEnCajaDelDia();
            if (cajaDelDia == null)
            {
                return BadRequest("Es necesario abrir una caja para hoy antes de registrar ventas.");
            }
            decimal totalVenta = detalleCarrito.Items.Sum(item => item.PrecioVenta * item.Cantidad);

            // Preparación de la entidad Venta con los datos necesarios
            VentaRequest ventaRequest = new VentaRequest
            {
                FechaVenta = DateTime.Now,
                TipoComprobante = "Boleta",
                IdUsuario = 1, // Suponiendo que este ID viene de la sesión del usuario o es un valor fijo por ahora
                NroComprobante = "FAC00", // Este valor podría generarse dinámicamente según tu lógica de negocio
                IdPersona = detalleCarrito.Persona.IdPersona, // Asumiendo que el IdCliente viene correctamente desde el front-end
                IdCaja = cajaDelDia.IdCaja     
             };

            // Intento de creación de la venta en el sistema
            var venta = _IVentaBussines.Create(ventaRequest);
            if (venta == null)
            {
                return StatusCode(500, "Error al crear la venta");
            }

            cajaDelDia.IngresosACaja += cajaDelDia.IngresosACaja;
            cajaDelDia.SaldoFinal = cajaDelDia.SaldoInicial + cajaDelDia.IngresosACaja;
            _ICajaRepository.Update(cajaDelDia);

            List<DetalleVentaRequest> listaDetalle = new List<DetalleVentaRequest>();
            foreach (var item in detalleCarrito.Items)
            {
                var kardexActual = _kardexRepository.GetById(item.libro.IdLibro);
                if (kardexActual == null || kardexActual.Stock < item.Cantidad)
                {
                    return BadRequest("No hay suficiente stock para el libro con ID " + item.libro.IdLibro);
                }
                kardexActual.Stock -= item.Cantidad; // Asegúrate de que esto no ponga el stock en negativo
                _kardexRepository.Update(kardexActual);
                // Aquí, se podría verificar el stock del item
                var detalleVentaRequest = new DetalleVentaRequest
                {
                    IdVentas = venta.IdVentas,
                    NombreProducto = item.libro.Titulo,
                    PrecioUnit = item.PrecioVenta,
                    IdLibro = item.libro.IdLibro,
                    Cantidad = item.Cantidad,
                    Importe = item.PrecioVenta * item.Cantidad,
                    Estado = "Pendiente" // Asumiendo un estado inicial para la venta
                                         // Agrega aquí más campos si son necesarios
                };
                listaDetalle.Add(detalleVentaRequest);
            }

            // Creación de los detalles de venta en el sistema
            var detallesCreados = _IDetalleVentaBussines.CreateMultiple(listaDetalle);
            if (detallesCreados == null)
            {
                return StatusCode(500, "Error al crear el detalle de la venta");
            }

            // Retorno de una respuesta exitosa con un mensaje de confirmación
            return Ok(new { Message = "Venta y detalles registrados con éxito" });
        }

        #endregion
    }
}
