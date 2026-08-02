using AutoMapper;
using Bussines;
using DBModel.DB;
using DocumentFormat.OpenXml.Vml.Office;
using IBussines;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    //[Authorize]
    public class DetalleVentaController : ControllerBase
    {
        #region Declaracion de vcariables generales
        private readonly IDetalleVentaBussines _detalleVentaBussines;
        private readonly IKardexBussines _kardexBussines;
        private readonly IDetalleVentaBussines _IDetalleVentaBussines = null;
        private readonly IVentaBussines _IVentaBussines = null;
        #endregion

        #region constructor 
        // NOTA: antes este controller recibía también IKardexRepository, ICajaRepository,
        // IPersonaBussines e ICajaBussines para orquestar manualmente (y sin transacción) el
        // registro de venta + caja + kardex en RegistrarVentaYDetalle. Esa orquestación ahora
        // vive en VentaBussines.RegistrarVentaConDetalleAsync, envuelta en una transacción real
        // (ver UnitOfWork.ExecuteInTransactionAsync), así que el controller ya no depende de
        // repositorios ni de otras capas de negocio: solo llama a IVentaBussines.
        public DetalleVentaController(IDetalleVentaBussines detalleVentaBussines, IKardexBussines kardexBussines, IVentaBussines ventaBussines)
        {
            _detalleVentaBussines = detalleVentaBussines;
            _kardexBussines = kardexBussines;
            _IDetalleVentaBussines = detalleVentaBussines;
            _IVentaBussines = ventaBussines;
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
        /// <summary>
        /// Obtiene los detalles de venta asociados a una persona.
        /// </summary>
        /// <param name="idPersona">Identificador de la persona.</param>
        /// <returns>Lista de detalles de venta de la persona.</returns>
        [HttpGet("traer/{idPersona}")]
        public async Task<IActionResult> GetDetalleVentasByPersonaId(int idPersona)
        {
                var detalleVentas = await _detalleVentaBussines.GetDetalleVentasByPersonaId(idPersona);

                return Ok(detalleVentas);
        }



        /// <summary>
        /// Registra una venta y sus detalles, creando la persona si no existe y actualizando stock y caja.
        /// </summary>
        /// <param name="detalleCarrito">Carrito con los ítems de la venta.</param>
        /// <returns>Mensaje de confirmación.</returns>
        [HttpPost("registrar-venta-detalle")]
        public async Task<IActionResult> RegistrarVentaYDetalle([FromBody] DatalleCarrito detalleCarrito)
        {
            try
            {
                // Toda la orquestación (persona, caja del día, venta, kardex y detalle de venta)
                // ocurre dentro de UNA transacción en VentaBussines.RegistrarVentaConDetalleAsync.
                // Si algo falla (p. ej. stock insuficiente en un ítem), se revierte todo.
                var resultado = await _IVentaBussines.RegistrarVentaConDetalleAsync(detalleCarrito);
                return Ok(resultado);
            }
            catch (InvalidOperationException ex)
            {
                // Errores de negocio esperables (sin caja abierta, sin stock, etc.)
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al registrar la venta: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los detalles de venta de una venta específica.
        /// </summary>
        /// <param name="idVenta">Identificador de la venta.</param>
        /// <returns>Lista de detalles de la venta.</returns>
        [HttpGet("GetByVenta/{idVenta}")]
        public async Task<IActionResult> GetDetallesByVenta(int idVenta)
        {
            var detalles = await _detalleVentaBussines.GetDetalleVentasByVentaId(idVenta);

            if (detalles == null || !detalles.Any())
            {
                return NotFound($"No se encontraron detalles de venta para la venta con ID {idVenta}.");
            }

            return Ok(detalles);
        }

        /// <summary>
        /// Actualiza el estado de los pedidos asociados a una venta y guarda imágenes.
        /// </summary>
        /// <param name="idVenta">Identificador de la venta.</param>
        /// <param name="request">Datos del estado del pedido.</param>
        /// <returns>Resultado de la actualización.</returns>
        [HttpPut("UpdateEstadoPedidos/{idVenta}")]
        public async Task<IActionResult> UpdateEstadoPedidos(int idVenta, [FromForm] EstadoPedidoRequest request)
        {
            try
            {
                // Obtener las imágenes desde la solicitud
                var images = Request.Form.Files.GetFiles("images").ToList();

                // Verificar si no se recibieron imágenes
                if (images == null || !images.Any())
                {
                    return BadRequest("No se recibieron imágenes en la solicitud.");
                }

                // Llamar al servicio de negocio para actualizar los estados y crear las imágenes
                var result = await _detalleVentaBussines.UpdateEstadoPedidosAndCreateImagenes(idVenta, request, images);

                if (!result)
                {
                    return NotFound($"No se encontraron detalles de venta para la venta con ID {idVenta}.");
                }

                return Ok($"Se actualizó el estado y se crearon las imágenes para la venta con ID {idVenta}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al procesar la solicitud: {ex.Message}");
            }
        }


        /// <summary>
        /// Obtiene el estado de un pedido a partir del detalle de venta.
        /// </summary>
        /// <param name="idDetalleVenta">Identificador del detalle de venta.</param>
        /// <returns>Estado del pedido.</returns>
        [HttpGet("GetEstadoPedido/{idDetalleVenta}")]
        public async Task<IActionResult> GetEstadoPedidoByDetalleVentaId(int idDetalleVenta)
        {
            var estadoPedido = await _detalleVentaBussines.GetEstadoPedidoByDetalleVentaIdAsync(idDetalleVenta);

            if (estadoPedido == null)
                return NotFound("Estado del pedido no encontrado");

            return Ok(estadoPedido);
        }
        /// <summary>
        /// Obtiene los productos más vendidos en un mes y año determinados.
        /// </summary>
        /// <param name="mes">Mes (1-12).</param>
        /// <param name="anio">Año (>=2000).</param>
        /// <returns>Lista de productos más vendidos.</returns>
        [HttpGet("productos-mas-vendidos")]
        public async Task<IActionResult> GetProductosMasVendidos([FromQuery] int mes, [FromQuery] int anio)
        {
            if (mes < 1 || mes > 12 || anio < 2000) // Validación básica
            {
                return BadRequest("Mes o año inválido");
            }

            var productos = await _detalleVentaBussines.ObtenerProductosMasVendidosDelMesAsync(mes, anio);
            return Ok(productos);
        }
        #endregion
    }
}
