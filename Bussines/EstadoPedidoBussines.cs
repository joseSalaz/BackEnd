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
    public class EstadoPedidoBussines : IEstadoPedidoBussines
    {
        #region Declaracion de vcariables generales
        public readonly IEstadoPedidoRepository _IEstadoPedidoRepository = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public EstadoPedidoBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IEstadoPedidoRepository = new EstadoPedidoRepository();
        }
        #endregion

        public EstadoPedidoResponse Create(EstadoPedidoRequest entity)
        {
            EstadoPedido au = _Mapper.Map<EstadoPedido>(entity);
            au = _IEstadoPedidoRepository.Create(au);
            EstadoPedidoResponse res = _Mapper.Map<EstadoPedidoResponse>(au);
            return res;
        }

        public List<EstadoPedidoResponse> CreateMultiple(List<EstadoPedidoRequest> request)
        {
            List<EstadoPedido> au = _Mapper.Map<List<EstadoPedido>>(request);
            au = _IEstadoPedidoRepository.InsertMultiple(au);
            List<EstadoPedidoResponse> res = _Mapper.Map<List<EstadoPedidoResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IEstadoPedidoRepository.Delete(id);
        }

        public int deleteMultipleItems(List<EstadoPedidoRequest> request)
        {
            List<EstadoPedido> au = _Mapper.Map<List<EstadoPedido>>(request);
            int cantidad = _IEstadoPedidoRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<EstadoPedidoResponse> getAll()
        {
            List<EstadoPedido> lsl = _IEstadoPedidoRepository.GetAll();
            List<EstadoPedidoResponse> res = _Mapper.Map<List<EstadoPedidoResponse>>(lsl);
            return res;
        }

        public List<EstadoPedidoResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public EstadoPedidoResponse getById(object id)
        {
            EstadoPedido au = _IEstadoPedidoRepository.GetById(id);
            EstadoPedidoResponse res = _Mapper.Map<EstadoPedidoResponse>(au);
            return res;
        }

        public EstadoPedidoResponse Update(EstadoPedidoRequest entity)
        {
            EstadoPedido au = _Mapper.Map<EstadoPedido>(entity);
            au = _IEstadoPedidoRepository.Update(au);
            EstadoPedidoResponse res = _Mapper.Map<EstadoPedidoResponse>(au);
            return res;
        }

        public List<EstadoPedidoResponse> UpdateMultiple(List<EstadoPedidoRequest> request)
        {
            List<EstadoPedido> au = _Mapper.Map<List<EstadoPedido>>(request);
            au = _IEstadoPedidoRepository.UpdateMultiple(au);
            List<EstadoPedidoResponse> res = _Mapper.Map<List<EstadoPedidoResponse>>(au);
            return res;
        }
    }
}
