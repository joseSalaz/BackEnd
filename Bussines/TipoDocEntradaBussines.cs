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
    public class TipoDocEntradaBussines  : ITipoDocEntradaBussines
    {
        #region Declaracion de vcariables generales
        public readonly ITipoDocEntradaRepository _ITipoDocEntradaRepository = null;
        public readonly IMapper _Mapper;

        public TipoDocEntradaBussines()
        {
        }
        #endregion

        #region constructor 
        public TipoDocEntradaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ITipoDocEntradaRepository = new TipoDocEntradaRepository();
        }
        #endregion

        public TipoDocEntradaResponse Create(TipoDocEntradaRequest entity)
        {
            TipoDocEntrada au = _Mapper.Map<TipoDocEntrada>(entity);
            au = _ITipoDocEntradaRepository.Create(au);
            TipoDocEntradaResponse res = _Mapper.Map<TipoDocEntradaResponse>(au);
            return res;
        }

        public List<TipoDocEntradaResponse> CreateMultiple(List<TipoDocEntradaRequest> request)
        {
            List<TipoDocEntrada> au = _Mapper.Map<List<TipoDocEntrada>>(request);
            au = _ITipoDocEntradaRepository.InsertMultiple(au);
            List<TipoDocEntradaResponse> res = _Mapper.Map<List<TipoDocEntradaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ITipoDocEntradaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<TipoDocEntradaRequest> request)
        {
            List<TipoDocEntrada> au = _Mapper.Map<List<TipoDocEntrada>>(request);
            int cantidad = _ITipoDocEntradaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<TipoDocEntradaResponse> getAll()
        {
            List<TipoDocEntrada> lsl = _ITipoDocEntradaRepository.GetAll();
            List<TipoDocEntradaResponse> res = _Mapper.Map<List<TipoDocEntradaResponse>>(lsl);
            return res;
        }

        public List<TipoDocEntradaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public TipoDocEntradaResponse getById(object id)
        {
            TipoDocEntrada au = _ITipoDocEntradaRepository.GetById(id);
            TipoDocEntradaResponse res = _Mapper.Map<TipoDocEntradaResponse>(au);
            return res;
        }

        public TipoDocEntradaResponse Update(TipoDocEntradaRequest entity)
        {
            TipoDocEntrada au = _Mapper.Map<TipoDocEntrada>(entity);
            au = _ITipoDocEntradaRepository.Update(au);
            TipoDocEntradaResponse res = _Mapper.Map<TipoDocEntradaResponse>(au);
            return res;
        }

        public List<TipoDocEntradaResponse> UpdateMultiple(List<TipoDocEntradaRequest> request)
        {
            List<TipoDocEntrada> au = _Mapper.Map<List<TipoDocEntrada>>(request);
            au = _ITipoDocEntradaRepository.UpdateMultiple(au);
            List<TipoDocEntradaResponse> res = _Mapper.Map<List<TipoDocEntradaResponse>>(au);
            return res;
        }
    }
}
