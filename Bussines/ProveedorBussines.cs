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
    public class ProveedorBussines: IProveedorBussines
    {
        #region Declaracion de vcariables generales
        public readonly IProveedorRepository _IProveedorRepository = null;
        public readonly IMapper _Mapper;

        public ProveedorBussines()
        {
        }
        #endregion

        #region constructor 
        public ProveedorBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IProveedorRepository = new ProveedorRepository();
        }
        #endregion

        public ProveedorResponse Create(ProveedorRequest entity)
        {
            Proveedor au = _Mapper.Map<Proveedor>(entity);
            au = _IProveedorRepository.Create(au);
            ProveedorResponse res = _Mapper.Map<ProveedorResponse>(au);
            return res;
        }

        public List<ProveedorResponse> CreateMultiple(List<ProveedorRequest> request)
        {
            List<Proveedor> au = _Mapper.Map<List<Proveedor>>(request);
            au = _IProveedorRepository.InsertMultiple(au);
            List<ProveedorResponse> res = _Mapper.Map<List<ProveedorResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IProveedorRepository.Delete(id);
        }

        public int deleteMultipleItems(List<ProveedorRequest> request)
        {
            List<Proveedor> au = _Mapper.Map<List<Proveedor>>(request);
            int cantidad = _IProveedorRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ProveedorResponse> getAll()
        {
            List<Proveedor> lsl = _IProveedorRepository.GetAll();
            List<ProveedorResponse> res = _Mapper.Map<List<ProveedorResponse>>(lsl);
            return res;
        }

        public List<ProveedorResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public ProveedorResponse getById(object id)
        {
            Proveedor au = _IProveedorRepository.GetById(id);
            ProveedorResponse res = _Mapper.Map<ProveedorResponse>(au);
            return res;
        }

        public ProveedorResponse Update(ProveedorRequest entity)
        {
            Proveedor au = _Mapper.Map<Proveedor>(entity);
            au = _IProveedorRepository.Update(au);
            ProveedorResponse res = _Mapper.Map<ProveedorResponse>(au);
            return res;
        }

        public List<ProveedorResponse> UpdateMultiple(List<ProveedorRequest> request)
        {
            List<Proveedor> au = _Mapper.Map<List<Proveedor>>(request);
            au = _IProveedorRepository.UpdateMultiple(au);
            List<ProveedorResponse> res = _Mapper.Map<List<ProveedorResponse>>(au);
            return res;
        }
    }
}
