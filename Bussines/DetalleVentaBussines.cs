using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepositorio;
using IRepository;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Models.RequestResponse;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilPDF;

namespace Bussines
{
    public class DetalleVentaBussines : IDetalleVentaBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDetalleVentaRepository _IDetalleVentaRepository;
        public readonly IMapper _Mapper;
        public readonly IEstadoPedidoImageneBussines _IEstadoPedidoImageneBussines;
        public readonly IEstadoPedidoRepository _estadoPedidoRepository;
        public readonly ILibroRepository _libroRepository;
        public readonly IEmailService _emailService;
        public readonly IVentaRepository _ventaRepository;
        #endregion

        #region constructor 
        public DetalleVentaBussines(IMapper mapper, IEstadoPedidoImageneBussines iEstadoPedidoImageneBussines,IEstadoPedidoRepository estadoPedidoRepository,
            IVentaRepository ventaRepository,ILibroRepository libroRepository,IEmailService emailService )
        {
            _Mapper = mapper;
            _IDetalleVentaRepository = new DetalleVentaRepository();
            _IEstadoPedidoImageneBussines = iEstadoPedidoImageneBussines;
            _estadoPedidoRepository = estadoPedidoRepository;
            _ventaRepository = ventaRepository;
            _emailService = emailService;
            _libroRepository = libroRepository;
        }
        #endregion

        public DetalleVentaResponse Create(DetalleVentaRequest entity)
        {
            DetalleVenta au = _Mapper.Map<DetalleVenta>(entity);
            au = _IDetalleVentaRepository.Create(au);
            DetalleVentaResponse res = _Mapper.Map<DetalleVentaResponse>(au);
            return res;
        }

        public List<DetalleVentaResponse> CreateMultiple(List<DetalleVentaRequest> request)
        {
            List<DetalleVenta> au = _Mapper.Map<List<DetalleVenta>>(request);
            au = _IDetalleVentaRepository.InsertMultiple(au);
            List<DetalleVentaResponse> res = _Mapper.Map<List<DetalleVentaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDetalleVentaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DetalleVentaRequest> request)
        {
            List<DetalleVenta> au = _Mapper.Map<List<DetalleVenta>>(request);
            int cantidad = _IDetalleVentaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DetalleVentaResponse> getAll()
        {
            List<DetalleVenta> lsl = _IDetalleVentaRepository.GetAll();
            List<DetalleVentaResponse> res = _Mapper.Map<List<DetalleVentaResponse>>(lsl);
            return res;
        }

        public List<DetalleVentaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DetalleVentaResponse getById(object id)
        {
            DetalleVenta au = _IDetalleVentaRepository.GetById(id);
            DetalleVentaResponse res = _Mapper.Map<DetalleVentaResponse>(au);
            return res;
        }

        public DetalleVentaResponse Update(DetalleVentaRequest entity)
        {
            DetalleVenta au = _Mapper.Map<DetalleVenta>(entity);
            au = _IDetalleVentaRepository.Update(au);
            DetalleVentaResponse res = _Mapper.Map<DetalleVentaResponse>(au);
            return res;
        }


        public List<DetalleVentaResponse> UpdateMultiple(List<DetalleVentaRequest> request)
        {
            List<DetalleVenta> au = _Mapper.Map<List<DetalleVenta>>(request);
            au = _IDetalleVentaRepository.UpdateMultiple(au);
            List<DetalleVentaResponse> res = _Mapper.Map<List<DetalleVentaResponse>>(au);
            return res;
        }
        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentasByPersonaId(int idPersona)
        {
            return await _IDetalleVentaRepository.GetDetalleVentasByPersonaId(idPersona);
        }

        public async Task<IEnumerable<DetalleVenta>> GetDetalleVentasByVentaId(int idVenta)
        {
            return await _IDetalleVentaRepository.GetDetalleVentasByVentaId(idVenta);
        }

        public async Task<bool> UpdateEstadoPedidosAndCreateImagenes(int idVenta, EstadoPedidoRequest request, List<IFormFile> images)
        {
            // Actualizamos los estados de los pedidos y obtenemos los IdEstadoPedido actualizados
            var estadoPedidoIds = await _IDetalleVentaRepository.UpdateEstadoPedidosByVentaId(idVenta, request);
            if (!estadoPedidoIds.Any())
                return false;

            // Obtener libros y productos relacionados con la venta
            var libros = (await _libroRepository.GetLibrosByVentaIdAsync(idVenta)).ToList();
            var productos = (await _IDetalleVentaRepository.GetDetalleVentasByVentaId(idVenta)).ToList();

            // Extraer imágenes de los productos
            var imagenesProductos = libros.Select(l => l.Imagen).Where(img => !string.IsNullOrEmpty(img)).ToList();

            // Crear registros en EstadoPedidoImagene y subir imágenes
            foreach (var estadoPedidoId in estadoPedidoIds)
            {
                var estadoPedidoImageneRequest = new EstadoPedidoImageneRequest
                {
                    IdEstadoPedido = estadoPedidoId, // IdEstadoPedido actualizado
                    Estado = request.Estado,
                    Fecha = request.FechaEstado // Fecha recibida
                };

                // Crear el registro con todas las imágenes asociadas
                await _IEstadoPedidoImageneBussines.CreateWithImagesAsync(estadoPedidoImageneRequest, images);
            }

            // Enviar notificación por email al cliente
            var clienteEmail = await _ventaRepository.GetEmailByVentaId(idVenta);
            if (!string.IsNullOrEmpty(clienteEmail))
            {
                await _emailService.SendOrderStatusUpdateEmailAsync(clienteEmail, idVenta, request.Estado, productos, imagenesProductos);
            }

            return true;
        }




        public async Task<EstadoPedido> GetEstadoPedidoByDetalleVentaIdAsync(int idDetalleVenta)
        {
            // Llama al repositorio para obtener el EstadoPedido por IdDetalleVenta
            return await _estadoPedidoRepository.GetEstadoPedidoByDetalleVentaIdAsync(idDetalleVenta);
        }

        public async Task<List<ProductosMasVendidosResponse>> ObtenerProductosMasVendidosDelMesAsync(int mes, int anio)
        {
            return await _IDetalleVentaRepository.ObtenerProductosMasVendidosAsync(mes, anio);
        }

    }
}
