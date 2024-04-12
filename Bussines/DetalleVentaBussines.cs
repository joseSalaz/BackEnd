using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepository;
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
      
        #endregion

        #region constructor 
        public DetalleVentaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDetalleVentaRepository = new DetalleVentaRepository();
           
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
    }
}
