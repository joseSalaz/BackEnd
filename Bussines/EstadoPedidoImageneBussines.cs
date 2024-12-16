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
    public class EstadoPedidoImageneBussines : IEstadoPedidoImageneBussines
    {
        #region Declaracion de vcariables generales
        public readonly IEstadoPedidoImageneRepository _IEstadoPedidoImageneRepository = null;
        public readonly IMapper _Mapper;

        public EstadoPedidoImageneBussines()
        {
        }
        #endregion

        #region constructor 
        public EstadoPedidoImageneBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IEstadoPedidoImageneRepository = new EstadoPedidoImageneRepository();
        }
        #endregion

        public EstadoPedidoImageneResponse Create(EstadoPedidoImageneRequest entity)
        {
            EstadoPedidoImagene au = _Mapper.Map<EstadoPedidoImagene>(entity);
            au = _IEstadoPedidoImageneRepository.Create(au);
            EstadoPedidoImageneResponse res = _Mapper.Map<EstadoPedidoImageneResponse>(au);
            return res;
        }

        public List<EstadoPedidoImageneResponse> CreateMultiple(List<EstadoPedidoImageneRequest> request)
        {
            List<EstadoPedidoImagene> au = _Mapper.Map<List<EstadoPedidoImagene>>(request);
            au = _IEstadoPedidoImageneRepository.InsertMultiple(au);
            List<EstadoPedidoImageneResponse> res = _Mapper.Map<List<EstadoPedidoImageneResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IEstadoPedidoImageneRepository.Delete(id);
        }

        public int deleteMultipleItems(List<EstadoPedidoImageneRequest> request)
        {
            List<EstadoPedidoImagene> au = _Mapper.Map<List<EstadoPedidoImagene>>(request);
            int cantidad = _IEstadoPedidoImageneRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<EstadoPedidoImageneResponse> getAll()
        {
            List<EstadoPedidoImagene> lsl = _IEstadoPedidoImageneRepository.GetAll();
            List<EstadoPedidoImageneResponse> res = _Mapper.Map<List<EstadoPedidoImageneResponse>>(lsl);
            return res;
        }

        public List<EstadoPedidoImageneResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public EstadoPedidoImageneResponse getById(object id)
        {
            EstadoPedidoImagene au = _IEstadoPedidoImageneRepository.GetById(id);
            EstadoPedidoImageneResponse res = _Mapper.Map<EstadoPedidoImageneResponse>(au);
            return res;
        }

        public EstadoPedidoImageneResponse Update(EstadoPedidoImageneRequest entity)
        {
            EstadoPedidoImagene au = _Mapper.Map<EstadoPedidoImagene>(entity);
            au = _IEstadoPedidoImageneRepository.Update(au);
            EstadoPedidoImageneResponse res = _Mapper.Map<EstadoPedidoImageneResponse>(au);
            return res;
        }

        public List<EstadoPedidoImageneResponse> UpdateMultiple(List<EstadoPedidoImageneRequest> request)
        {
            List<EstadoPedidoImagene> au = _Mapper.Map<List<EstadoPedidoImagene>>(request);
            au = _IEstadoPedidoImageneRepository.UpdateMultiple(au);
            List<EstadoPedidoImageneResponse> res = _Mapper.Map<List<EstadoPedidoImageneResponse>>(au);
            return res;
        }
    }
}
