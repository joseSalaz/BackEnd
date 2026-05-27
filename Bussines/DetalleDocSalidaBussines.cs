using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepository;
using Models.RequestResponse;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class DetalleDocSalidaBussines : IDetalleDocSalidaBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDetalleDocSalidaRepository _IDetalleDocSalidaRepository = null;
        public readonly IMapper _Mapper;

        public DetalleDocSalidaBussines()
        {
        }
        #endregion

        #region constructor 
        public DetalleDocSalidaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDetalleDocSalidaRepository = new DetalleDocSalidaRepository();
        }
        #endregion

        public DetalleDocSalidaResponse Create(DetalleDocSalidaRequest entity)
        {
            DetalleDocSalida au = _Mapper.Map<DetalleDocSalida>(entity);
            au = _IDetalleDocSalidaRepository.Create(au);
            DetalleDocSalidaResponse res = _Mapper.Map<DetalleDocSalidaResponse>(au);
            return res;
        }

        public List<DetalleDocSalidaResponse> CreateMultiple(List<DetalleDocSalidaRequest> request)
        {
            List<DetalleDocSalida> au = _Mapper.Map<List<DetalleDocSalida>>(request);
            au = _IDetalleDocSalidaRepository.InsertMultiple(au);
            List<DetalleDocSalidaResponse> res = _Mapper.Map<List<DetalleDocSalidaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDetalleDocSalidaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DetalleDocSalidaRequest> request)
        {
            List<DetalleDocSalida> au = _Mapper.Map<List<DetalleDocSalida>>(request);
            int cantidad = _IDetalleDocSalidaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DetalleDocSalidaResponse> getAll()
        {
            List<DetalleDocSalida> lsl = _IDetalleDocSalidaRepository.GetAll();
            List<DetalleDocSalidaResponse> res = _Mapper.Map<List<DetalleDocSalidaResponse>>(lsl);
            return res;
        }

        public List<DetalleDocSalidaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DetalleDocSalidaResponse getById(object id)
        {
            DetalleDocSalida au = _IDetalleDocSalidaRepository.GetById(id);
            DetalleDocSalidaResponse res = _Mapper.Map<DetalleDocSalidaResponse>(au);
            return res;
        }

        public DetalleDocSalidaResponse Update(DetalleDocSalidaRequest entity)
        {
            DetalleDocSalida au = _Mapper.Map<DetalleDocSalida>(entity);
            au = _IDetalleDocSalidaRepository.Update(au);
            DetalleDocSalidaResponse res = _Mapper.Map<DetalleDocSalidaResponse>(au);
            return res;
        }

        public List<DetalleDocSalidaResponse> UpdateMultiple(List<DetalleDocSalidaRequest> request)
        {
            List<DetalleDocSalida> au = _Mapper.Map<List<DetalleDocSalida>>(request);
            au = _IDetalleDocSalidaRepository.UpdateMultiple(au);
            List<DetalleDocSalidaResponse> res = _Mapper.Map<List<DetalleDocSalidaResponse>>(au);
            return res;
        }
    }
}
