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
    public class TipoPapelBussines : ITipoPapelBussines
    {
        #region Declaracion de vcariables generales
        public readonly ITipoPapelRepository _ITipoPapelRepository = null;
        public readonly IMapper _Mapper;

        public TipoPapelBussines()
        {
        }
        #endregion

        #region constructor 
        public TipoPapelBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ITipoPapelRepository = new TipoPapelRepository();
        }
        #endregion

        public TipoPapelResponse Create(TipoPapelRequest entity)
        {
            TipoPapel au = _Mapper.Map<TipoPapel>(entity);
            au = _ITipoPapelRepository.Create(au);
            TipoPapelResponse res = _Mapper.Map<TipoPapelResponse>(au);
            return res;
        }

        public List<TipoPapelResponse> CreateMultiple(List<TipoPapelRequest> request)
        {
            List<TipoPapel> au = _Mapper.Map<List<TipoPapel>>(request);
            au = _ITipoPapelRepository.InsertMultiple(au);
            List<TipoPapelResponse> res = _Mapper.Map<List<TipoPapelResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ITipoPapelRepository.Delete(id);
        }

        public int deleteMultipleItems(List<TipoPapelRequest> request)
        {
            List<TipoPapel> au = _Mapper.Map<List<TipoPapel>>(request);
            int cantidad = _ITipoPapelRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<TipoPapelResponse> getAll()
        {
            List<TipoPapel> lsl = _ITipoPapelRepository.GetAll();
            List<TipoPapelResponse> res = _Mapper.Map<List<TipoPapelResponse>>(lsl);
            return res;
        }

        public List<TipoPapelResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public TipoPapelResponse getById(object id)
        {
            TipoPapel au = _ITipoPapelRepository.GetById(id);
            TipoPapelResponse res = _Mapper.Map<TipoPapelResponse>(au);
            return res;
        }

        public TipoPapelResponse Update(TipoPapelRequest entity)
        {
            TipoPapel au = _Mapper.Map<TipoPapel>(entity);
            au = _ITipoPapelRepository.Update(au);
            TipoPapelResponse res = _Mapper.Map<TipoPapelResponse>(au);
            return res;
        }

        public List<TipoPapelResponse> UpdateMultiple(List<TipoPapelRequest> request)
        {
            List<TipoPapel> au = _Mapper.Map<List<TipoPapel>>(request);
            au = _ITipoPapelRepository.UpdateMultiple(au);
            List<TipoPapelResponse> res = _Mapper.Map<List<TipoPapelResponse>>(au);
            return res;
        }
    }
}
