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
    public class PublicoObjetivoBussines : IPublicoObjetivoBussines
    {
        #region Declaracion de vcariables generales
        public readonly IPublicoObjetivoRepository _IPublicoObjetivoRepository = null;
        public readonly IMapper _Mapper;

        public PublicoObjetivoBussines()
        {
        }
        #endregion

        #region constructor 
        public PublicoObjetivoBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IPublicoObjetivoRepository = new PublicoObjetivoRepository();
        }
        #endregion

        public PublicoObjetivoResponse Create(PublicoObjetivoRequest entity)
        {
            PublicoObjetivo au = _Mapper.Map<PublicoObjetivo>(entity);
            au = _IPublicoObjetivoRepository.Create(au);
            PublicoObjetivoResponse res = _Mapper.Map<PublicoObjetivoResponse>(au);
            return res;
        }

        public List<PublicoObjetivoResponse> CreateMultiple(List<PublicoObjetivoRequest> request)
        {
            List<PublicoObjetivo> au = _Mapper.Map<List<PublicoObjetivo>>(request);
            au = _IPublicoObjetivoRepository.InsertMultiple(au);
            List<PublicoObjetivoResponse> res = _Mapper.Map<List<PublicoObjetivoResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IPublicoObjetivoRepository.Delete(id);
        }

        public int deleteMultipleItems(List<PublicoObjetivoRequest> request)
        {
            List<PublicoObjetivo> au = _Mapper.Map<List<PublicoObjetivo>>(request);
            int cantidad = _IPublicoObjetivoRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PublicoObjetivoResponse> getAll()
        {
            List<PublicoObjetivo> lsl = _IPublicoObjetivoRepository.GetAll();
            List<PublicoObjetivoResponse> res = _Mapper.Map<List<PublicoObjetivoResponse>>(lsl);
            return res;
        }

        public List<PublicoObjetivoResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PublicoObjetivoResponse getById(object id)
        {
            PublicoObjetivo au = _IPublicoObjetivoRepository.GetById(id);
            PublicoObjetivoResponse res = _Mapper.Map<PublicoObjetivoResponse>(au);
            return res;
        }

        public PublicoObjetivoResponse Update(PublicoObjetivoRequest entity)
        {
            PublicoObjetivo au = _Mapper.Map<PublicoObjetivo>(entity);
            au = _IPublicoObjetivoRepository.Update(au);
            PublicoObjetivoResponse res = _Mapper.Map<PublicoObjetivoResponse>(au);
            return res;
        }

        public List<PublicoObjetivoResponse> UpdateMultiple(List<PublicoObjetivoRequest> request)
        {
            List<PublicoObjetivo> au = _Mapper.Map<List<PublicoObjetivo>>(request);
            au = _IPublicoObjetivoRepository.UpdateMultiple(au);
            List<PublicoObjetivoResponse> res = _Mapper.Map<List<PublicoObjetivoResponse>>(au);
            return res;
        }
    }
}
