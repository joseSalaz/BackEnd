using AutoMapper;
using Bussines;
using DBModel.DB;
using IBussines;
using IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class VentaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        public readonly IVentaBussines _IVentaBussines = null;
        public readonly IMapper _Mapper;
        public readonly IEmailService _emailService;
        #endregion

        #region constructor 
        public VentaController(IMapper mapper,IEmailService emailService)
        {
            _Mapper = mapper;
            _emailService = emailService;
            _IVentaBussines = new VentaBussines(_Mapper, _emailService);
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
            List<VentaResponse> lsl = _IVentaBussines.getAll();
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
            VentaResponse res = _IVentaBussines.getById(id);
            return Ok(res);
        }

        /// <summary>
        /// Inserta un nuevo registro
        /// </summary>
        /// <param name="request">Registro a insertar</param>
        /// <returns>Retorna el registro insertado</returns>
        [HttpPost]
        public IActionResult Create([FromBody] VentaRequest request)
        {
            VentaResponse res = _IVentaBussines.Create(request);
            return Ok(res);
        }

        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="entity">registro a actualizar</param>
        /// <returns>retorna el registro Actualiza</returns>
        [HttpPut]
        public IActionResult Update([FromBody] VentaRequest request)
        {
            VentaResponse res = _IVentaBussines.Update(request);
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
            int res = _IVentaBussines.Delete(id);
            return Ok(res);
        }


        [HttpGet("{idVenta}/detallesVenta")]
        public async Task<IActionResult> GetDetalleVentaByVentaId(int idVenta)
        {
            var detallesVenta = await _IVentaBussines.GetDetalleVentaByVentaId(idVenta);
            if (detallesVenta == null || detallesVenta.Count == 0)
            {
                return NotFound("No se encontraron detalles para la venta especificada.");
            }
            return Ok(detallesVenta);
        }

        [HttpGet("{idVenta}/pdf")]
        public async Task<IActionResult> GetVentaPdf(int idVenta)
        {
            try
            {
                MemoryStream pdfStream = await _IVentaBussines.CreateVentaPdf(idVenta);
                if (pdfStream == null || pdfStream.Length == 0)
                {
                    return NotFound("PDF no pudo ser generado.");
                }

                // Devuelve el archivo PDF.
                string filename = $"Venta_{idVenta}.pdf";
                return File(pdfStream, "application/pdf", filename);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObtenerPorFechas")]
        public async Task<IActionResult> ObtenerPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = await _IVentaBussines.ObtenerVentasPorFechaAsync(fechaInicio, fechaFin);
            if (!ventas.Any())
            {
                return NotFound("No se encontraron ventas en el rango de fechas especificado.");
            }
            return Ok(ventas);
        }

        [HttpGet("Paginator")]
        public async Task<IActionResult> GetAll(
     [FromQuery] int page = 1,
     [FromQuery] int pageSize = 10,
     [FromQuery] string estado = null,
     [FromQuery] bool ordenarPorFechaDesc = true,
     [FromQuery] DateTime? fechaInicio = null, 
     [FromQuery] DateTime? fechaFin = null
 )
        {
            try
            {
                var (ventaResponse, totalItems) = await _IVentaBussines.GetVentaPaginados(page, pageSize, estado, ordenarPorFechaDesc, fechaInicio, fechaFin);

                var response = new
                {
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    Ventas = ventaResponse
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        [HttpGet("GetVentaConDetallesYEstado/{idVenta}")]
        public async Task<IActionResult> GetVentaConDetallesYEstado(int idVenta)
        {
            var result = await _IVentaBussines.GetVentaConDetallesYEstado(idVenta);

            if (result.venta == null)
                return NotFound("Venta no encontrada.");

            return Ok(new
            {
                Venta = result.venta,
                Detalles = result.detalles,
                EstadoPedido = result.estado
            });
        }
        [HttpGet("persona/{idPersona}")]
        public async Task<ActionResult<List<Venta>>> ObtenerVentasPorIdPersona(int idPersona)
        {
            var ventas = await _IVentaBussines.ObtenerVentasPorIdPersona(idPersona);
            if (ventas == null || ventas.Count == 0) return NotFound("No se encontraron ventas.");
            return Ok(ventas);
        }

        // Obtener detalles de venta por ID de venta
        [HttpGet("detalle/{idVenta}")]
        public async Task<ActionResult<List<DetalleVenta>>> ObtenerDetallesPorIdVenta(int idVenta)
        {
            var detalles = await _IVentaBussines.ObtenerDetallesPorIdVenta(idVenta);
            if (detalles == null || detalles.Count == 0) return NotFound("No se encontraron detalles de venta.");
            return Ok(detalles);
        }

        // Obtener el estado del pedido por ID de detalle de venta (solo uno)
        [HttpGet("estado/{idDetalleVenta}")]
        public async Task<ActionResult<EstadoPedido>> ObtenerEstadoPedidoUnicoPorVenta(int idDetalleVenta)
        {
            var estadoPedido = await _IVentaBussines.ObtenerEstadoPedidoUnicoPorVenta(idDetalleVenta);
            if (estadoPedido == null) return NotFound("No se encontró estado del pedido.");
            return Ok(estadoPedido);
        }

        [HttpGet("detallePersona/{idVenta}")]
        public async Task<IActionResult> GetVentaConPersonaYDireccion(int idVenta)
        {
            var venta = await _IVentaBussines.GetVentaConPersonaYDireccion(idVenta);

            if (venta == null)
                return NotFound(new { message = "Venta no encontrada." });

            return Ok(venta);
        }


        [HttpGet("ingresos-mensuales")]
        public async Task<ActionResult<List<IngresoMensualResponse>>> ObtenerIngresosMensuales(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            var ingresos = await _IVentaBussines.ObtenerIngresosMensuales(fechaInicio, fechaFin);
            return Ok(ingresos);
        }


        [HttpGet("ventas-por-mes")]
        public async Task<IActionResult> ObtenerVentasPorMes([FromQuery] int anio, [FromQuery] int mes)
        {
            var ventas = await _IVentaBussines.ObtenerVentasPorMes(anio, mes);
            return Ok(ventas);
        }

        [HttpGet("reporte-excel")]
        public async Task<IActionResult> GenerarReporteVentasExcel([FromQuery] int anio, [FromQuery] int mes)
        {
            try
            {
                int anioActual = DateTime.Now.Year;
                int mesActual = DateTime.Now.Month;

                // Validación para evitar generar reportes de meses futuros
                if (anio > DateTime.Now.Year || mes < 1 || mes > 12)
                {
                    return BadRequest(new { mensaje = "No se pueden generar reportes de meses futuros." });
                }
                var archivoExcel = await _IVentaBussines.GenerarReporteVentasExcel(anio, mes);
                return File(archivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte_Ventas_{anio}_{mes}.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        #endregion
    }
}
