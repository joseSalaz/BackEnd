using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepositorio;
using IRepository;
using Microsoft.AspNetCore.Http;
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

        #endregion

        #region constructor 
        public DetalleVentaBussines(IMapper mapper, IEstadoPedidoImageneBussines iEstadoPedidoImageneBussines,IEstadoPedidoRepository estadoPedidoRepository)
        {
            _Mapper = mapper;
            _IDetalleVentaRepository = new DetalleVentaRepository();
            _IEstadoPedidoImageneBussines = iEstadoPedidoImageneBussines;
            _estadoPedidoRepository = estadoPedidoRepository;
            
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
            // Actualizar los estados de los pedidos en la tabla EstadoPedido
            var result = await _IDetalleVentaRepository.UpdateEstadoPedidosByVentaId(idVenta, request);
            if (!result)
                return false;

            // Ahora crear los registros correspondientes en EstadoPedidoImagene
            var estadoPedidoImageneRequest = new EstadoPedidoImageneRequest
            {
                IdEstadoPedido = request.IdEstadoPedido,  // Este es el IdEstadoPedido que se está actualizando
                Estado = request.Estado,
                Fecha = request.FechaEstado  // Usar la fecha que se está recibiendo
            };

            // Llamamos al servicio para crear las imágenes
            foreach (var image in images)
            {
                await _IEstadoPedidoImageneBussines.CreateWithImagesAsync(estadoPedidoImageneRequest, images);
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
