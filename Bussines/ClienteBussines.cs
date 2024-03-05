
using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepositorio;
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
    public class ClienteBussines: IClienteBussines
    {
        #region Declaracion de vcariables generales
        public readonly IClienteRepository _IClienteRepository = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public ClienteBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IClienteRepository = new ClienteRepository();
        }
        #endregion

        public ClienteResponse Create(ClienteRequest entity)
        {
            Cliente au = _Mapper.Map<Cliente>(entity);
            au = _IClienteRepository.Create(au);
            ClienteResponse res = _Mapper.Map<ClienteResponse>(au);
            return res;
        }

        public List<ClienteResponse> CreateMultiple(List<ClienteRequest> request)
        {
            List<Cliente> au = _Mapper.Map<List<Cliente>>(request);
            au = _IClienteRepository.InsertMultiple(au);
            List<ClienteResponse> res = _Mapper.Map<List<ClienteResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IClienteRepository.Delete(id);
        }

        public int deleteMultipleItems(List<ClienteRequest> request)
        {
            List<Cliente> au = _Mapper.Map<List<Cliente>>(request);
            int cantidad = _IClienteRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ClienteResponse> getAll()
        {
            List<Cliente> lsl = _IClienteRepository.GetAll();
            List<ClienteResponse> res = _Mapper.Map<List<ClienteResponse>>(lsl);
            return res;
        }

        public List<ClienteResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public ClienteResponse getById(object id)
        {
            Cliente au = _IClienteRepository.GetById(id);
            ClienteResponse res = _Mapper.Map<ClienteResponse>(au);
            return res;
        }

        public ClienteResponse Update(ClienteRequest entity)
        {
            Cliente au = _Mapper.Map<Cliente>(entity);
            au = _IClienteRepository.Update(au);
            ClienteResponse res = _Mapper.Map<ClienteResponse>(au);
            return res;
        }

        public List<ClienteResponse> UpdateMultiple(List<ClienteRequest> request)
        {
            List<Cliente> au = _Mapper.Map<List<Cliente>>(request);
            au = _IClienteRepository.UpdateMultiple(au);
            List<ClienteResponse> res = _Mapper.Map<List<ClienteResponse>>(au);
            return res;
        }
    }
}

