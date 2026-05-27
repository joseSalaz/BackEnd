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
    public class TipoDocSalidaBussines : ITipoDocSalidaBussines
    {
        #region Declaracion de vcariables generales
        public readonly ITipoDocSalidaRepository _ITipoDocSalidaRepository = null;
        public readonly IMapper _Mapper;

        public TipoDocSalidaBussines()
        {
        }
        #endregion

        #region constructor 
        public TipoDocSalidaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ITipoDocSalidaRepository = new TipoDocSalidaRepository();
        }
        #endregion

        public TipoDocSalidaResponse Create(TipoDocSalidaRequest entity)
        {
            TipoDocSalida au = _Mapper.Map<TipoDocSalida>(entity);
            au = _ITipoDocSalidaRepository.Create(au);
            TipoDocSalidaResponse res = _Mapper.Map<TipoDocSalidaResponse>(au);
            return res;
        }

        public List<TipoDocSalidaResponse> CreateMultiple(List<TipoDocSalidaRequest> request)
        {
            List<TipoDocSalida> au = _Mapper.Map<List<TipoDocSalida>>(request);
            au = _ITipoDocSalidaRepository.InsertMultiple(au);
            List<TipoDocSalidaResponse> res = _Mapper.Map<List<TipoDocSalidaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ITipoDocSalidaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<TipoDocSalidaRequest> request)
        {
            List<TipoDocSalida> au = _Mapper.Map<List<TipoDocSalida>>(request);
            int cantidad = _ITipoDocSalidaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<TipoDocSalidaResponse> getAll()
        {
            List<TipoDocSalida> lsl = _ITipoDocSalidaRepository.GetAll();
            List<TipoDocSalidaResponse> res = _Mapper.Map<List<TipoDocSalidaResponse>>(lsl);
            return res;
        }

        public List<TipoDocSalidaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public TipoDocSalidaResponse getById(object id)
        {
            TipoDocSalida au = _ITipoDocSalidaRepository.GetById(id);
            TipoDocSalidaResponse res = _Mapper.Map<TipoDocSalidaResponse>(au);
            return res;
        }

        public TipoDocSalidaResponse Update(TipoDocSalidaRequest entity)
        {
            TipoDocSalida au = _Mapper.Map<TipoDocSalida>(entity);
            au = _ITipoDocSalidaRepository.Update(au);
            TipoDocSalidaResponse res = _Mapper.Map<TipoDocSalidaResponse>(au);
            return res;
        }

        public List<TipoDocSalidaResponse> UpdateMultiple(List<TipoDocSalidaRequest> request)
        {
            List<TipoDocSalida> au = _Mapper.Map<List<TipoDocSalida>>(request);
            au = _ITipoDocSalidaRepository.UpdateMultiple(au);
            List<TipoDocSalidaResponse> res = _Mapper.Map<List<TipoDocSalidaResponse>>(au);
            return res;
        }
    }
}
