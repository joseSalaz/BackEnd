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
    public class SucursalBussines :ISucursalBussines
    {
        #region Declaracion de vcariables generales
        public readonly ISucursalRepository _ISucursalRepository = null;
        public readonly IMapper _Mapper;

        public SucursalBussines()
        {
        }
        #endregion

        #region constructor 
        public SucursalBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ISucursalRepository = new SucursalRepository();
        }
        #endregion

        public SucursalResponse Create(SucursalRequest entity)
        {
            Sucursal au = _Mapper.Map<Sucursal>(entity);
            au = _ISucursalRepository.Create(au);
            SucursalResponse res = _Mapper.Map<SucursalResponse>(au);
            return res;
        }

        public List<SucursalResponse> CreateMultiple(List<SucursalRequest> request)
        {
            List<Sucursal> au = _Mapper.Map<List<Sucursal>>(request);
            au = _ISucursalRepository.InsertMultiple(au);
            List<SucursalResponse> res = _Mapper.Map<List<SucursalResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ISucursalRepository.Delete(id);
        }

        public int deleteMultipleItems(List<SucursalRequest> request)
        {
            List<Sucursal> au = _Mapper.Map<List<Sucursal>>(request);
            int cantidad = _ISucursalRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SucursalResponse> getAll()
        {
            List<Sucursal> lsl = _ISucursalRepository.GetAll();
            List<SucursalResponse> res = _Mapper.Map<List<SucursalResponse>>(lsl);
            return res;
        }

        public List<SucursalResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public SucursalResponse getById(object id)
        {
            Sucursal au = _ISucursalRepository.GetById(id);
            SucursalResponse res = _Mapper.Map<SucursalResponse>(au);
            return res;
        }

        public SucursalResponse Update(SucursalRequest entity)
        {
            Sucursal au = _Mapper.Map<Sucursal>(entity);
            au = _ISucursalRepository.Update(au);
            SucursalResponse res = _Mapper.Map<SucursalResponse>(au);
            return res;
        }

        public List<SucursalResponse> UpdateMultiple(List<SucursalRequest> request)
        {
            List<Sucursal> au = _Mapper.Map<List<Sucursal>>(request);
            au = _ISucursalRepository.UpdateMultiple(au);
            List<SucursalResponse> res = _Mapper.Map<List<SucursalResponse>>(au);
            return res;
        }
    }
}
