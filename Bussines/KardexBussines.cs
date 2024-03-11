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
    public class KardexBussines : IKardexBussines
    {
        #region Declaracion de vcariables generales
        public readonly IKardexRepository _IKardexRepository = null;
        public readonly IMapper _Mapper;

        public KardexBussines()
        {
        }
        #endregion

        #region constructor 
        public KardexBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IKardexRepository = new KardexRepository();
        }
        #endregion

        public KardexResponse Create(KardexRequest entity)
        {
            Kardex au = _Mapper.Map<Kardex>(entity);
            au = _IKardexRepository.Create(au);
            KardexResponse res = _Mapper.Map<KardexResponse>(au);
            return res;
        }

        public List<KardexResponse> CreateMultiple(List<KardexRequest> request)
        {
            List<Kardex> au = _Mapper.Map<List<Kardex>>(request);
            au = _IKardexRepository.InsertMultiple(au);
            List<KardexResponse> res = _Mapper.Map<List<KardexResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IKardexRepository.Delete(id);
        }

        public int deleteMultipleItems(List<KardexRequest> request)
        {
            List<Kardex> au = _Mapper.Map<List<Kardex>>(request);
            int cantidad = _IKardexRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<KardexResponse> getAll()
        {
            List<Kardex> lsl = _IKardexRepository.GetAll();
            List<KardexResponse> res = _Mapper.Map<List<KardexResponse>>(lsl);
            return res;
        }

        public List<KardexResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public KardexResponse getById(object id)
        {
            Kardex au = _IKardexRepository.GetById(id);
            KardexResponse res = _Mapper.Map<KardexResponse>(au);
            return res;
        }

        public KardexResponse Update(KardexRequest entity)
        {
            Kardex au = _Mapper.Map<Kardex>(entity);
            au = _IKardexRepository.Update(au);
            KardexResponse res = _Mapper.Map<KardexResponse>(au);
            return res;
        }

        public List<KardexResponse> UpdateMultiple(List<KardexRequest> request)
        {
            List<Kardex> au = _Mapper.Map<List<Kardex>>(request);
            au = _IKardexRepository.UpdateMultiple(au);
            List<KardexResponse> res = _Mapper.Map<List<KardexResponse>>(au);
            return res;
        }
    }
}
