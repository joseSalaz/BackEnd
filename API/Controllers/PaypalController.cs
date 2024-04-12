﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using Models.RequestResponse;
using IService;
using DBModel.DB;
using DocumentFormat.OpenXml.InkML;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using IRepository;
using Repository;
using Bussines;
using IBussines;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.CustomUI;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaypalController : ControllerBase
    {
        private readonly IApisPaypalServices _apisPaypalServices;
        private readonly IConfiguration _configuration;
        private readonly IKardexRepository _kardexRepository;
        private readonly IKardexBussines _kardexBussines;
        private readonly IDetalleVentaBussines _IDetalleVentaBussines = null;
        private readonly IVentaBussines _IVentaBussines = null;
        private readonly IMapper _mapper;
        public PaypalController(IApisPaypalServices apisPaypalServices, IConfiguration configuration, IKardexRepository kardexRepository, IKardexBussines kardexBussines, IMapper mapper, IDetalleVentaBussines detalleVentaBussines, IVentaBussines ventaBussines)
        {
            _apisPaypalServices = apisPaypalServices;
            _configuration = configuration;
            _kardexRepository = kardexRepository;
            _kardexBussines = kardexBussines;
            _mapper = mapper;
            _IDetalleVentaBussines = detalleVentaBussines;
            _IVentaBussines = ventaBussines;
        }


        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreationRequest paymentRequest)
        {
            try
            {
                // Suponiendo que `DatalleCarrito` se construye correctamente desde el `paymentRequest`
                DatalleCarrito detalleCarrito = new DatalleCarrito
                {
                    // Construir el detalle del carrito aquí
                };

                string returnUrl = $"{_configuration["http://localhost:4200"]}/respuesta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige después del pago exitoso
                string cancelUrl = $"{_configuration["http://localhost:4200"]}/respuesta"; // Esta debe ser la URL de tu frontend a donde PayPal redirige si el usuario cancela el pago

                var payment = await _apisPaypalServices.CreateOrdersasync(detalleCarrito, paymentRequest.Amount, returnUrl, cancelUrl);
                var approvalUrl = payment.links.FirstOrDefault(lnk => lnk.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;

                if (string.IsNullOrWhiteSpace(approvalUrl))
                {
                    return BadRequest("No se pudo obtener la URL de aprobación de PayPal.");
                }

                return Ok(new { PaymentId = payment.id, ApprovalUrl = approvalUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno al crear el pago: " + ex.Message);
            }
        }


        [HttpPost("execute-payment")]
        public async Task<IActionResult> ExecutePayment([FromBody] ExecutePaymentModelRequest paymentRequest)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(
                _configuration["PayPalSettings:ClientId"],
                _configuration["PayPalSettings:Secret"]
            ).GetAccessToken());

            var paymentExecution = new PaymentExecution { payer_id = paymentRequest.PayerID };
            var payment = new Payment { id = paymentRequest.PaymentId };

            try
            {
                var executedPayment = payment.Execute(apiContext, paymentExecution);
                if (executedPayment.state.ToLower() == "approved")
                {
                    await RegistrarVentaYDetalle(paymentRequest);
                    // No necesitas llamar a SaveChanges si tu método Update ya lo hace internamente
                    return Ok(new { PaymentId = executedPayment.id });
                }
                else
                {
                    return BadRequest("El pago no fue aprobado.");
                }
            }
            catch (Exception ex)
            {
                // Registro de errores y respuesta adecuada
                return StatusCode(500, "Error al ejecutar el pago: " + ex.Message);
            }
        }






        //private async Task<IActionResult> ProcesarPagoEnEfectivo(ExecutePaymentModelRequest paymentRequest)
        //{
        //    await RegistrarVentaYDetalle(paymentRequest);
        //    return Ok("Procesado con Exito");
        //}
        private async Task<IActionResult> RegistrarVentaYDetalle(ExecutePaymentModelRequest paymentRequest)
            {

            VentaRequest ventaRequest = new VentaRequest
                {
                    //IdCliente = userId,
                    //TotalPrecio = paymentRequest.Amount, // Asumiendo que existe un campo Total en ExecutePaymentModelRequest.
                    FechaVenta = DateTime.Now,
                    TipoComprobante = "Boleta",
                    IdUsuario = 8,
                    NroComprobante = "FAC00",//por ver
                    IdPersona =paymentRequest.Carrito.Persona.IdPersona,//Por Ver  paymentRequest.Carrito.IdCliente,

        // Otros campos necesarios...
    };
                var venta = _IVentaBussines.Create(ventaRequest);
                if (venta == null)
                {
                    return StatusCode(500, "Error al crear la venta");
                }
                List<DetalleVentaRequest> listaDetalle = new List<DetalleVentaRequest>();
                foreach (var item in paymentRequest.Carrito.Items)
                {
                    var kardexActual = _kardexRepository.GetById(item.libro.IdLibro);
                    if (kardexActual == null || kardexActual.Stock < item.Cantidad)
                    {
                        return BadRequest("No hay suficiente stock para el libro con ID " + item.libro.IdLibro);
                    }

                    // Actualiza el stock uno por uno
                    kardexActual.Stock -= item.Cantidad; // Asegúrate de que esto no ponga el stock en negativo
                    _kardexRepository.Update(kardexActual); // Utiliza tu método Update del repositorio
                    DetalleVentaRequest detalleventarequest = new DetalleVentaRequest
                    {
                        IdVentas = venta.IdVentas,
                        NombreProducto = item.libro.Titulo,
                        PrecioUnit = item.PrecioVenta,
                        IdLibro = item.libro.IdLibro,
                        Cantidad = item.Cantidad,
                        Importe = item.PrecioVenta * item.Cantidad,
                        Estado="Pendiente",
                        // otros campos necesarios...
                    };
                    listaDetalle.Add(detalleventarequest);
                }
                _IDetalleVentaBussines.CreateMultiple(listaDetalle);
                if (listaDetalle == null)
                {
                    return StatusCode(500, "error al crear el detalle de la venta" + listaDetalle);
                }
                return Ok(new { Message = "Venta y detalles registrados con éxito" });

            }
        }
    }


