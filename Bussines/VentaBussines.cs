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

    public class VentaBussines : IVentaBussines

    {
        #region Declaracion de vcariables generales
        public readonly IVentaRepository _IVentaRepository = null;
        public readonly IMapper _Mapper;

        public VentaBussines()
        {
        }
        #endregion

        #region constructor 
        public VentaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IVentaRepository = new VentaRepository();
        }
        #endregion

        public VentaResponse Create(VentaRequest entity)
        {
            Venta au = _Mapper.Map<Venta>(entity);
            au = _IVentaRepository.Create(au);
            VentaResponse res = _Mapper.Map<VentaResponse>(au);
            return res;
        }

        public List<VentaResponse> CreateMultiple(List<VentaRequest> request)
        {
            List<Venta> au = _Mapper.Map<List<Venta>>(request);
            au = _IVentaRepository.InsertMultiple(au);
            List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IVentaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<VentaRequest> request)
        {
            List<Venta> au = _Mapper.Map<List<Venta>>(request);
            int cantidad = _IVentaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<VentaResponse> getAll()
        {
            List<Venta> lsl = _IVentaRepository.GetAll();
            List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(lsl);
            return res;
        }

        public List<VentaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public VentaResponse getById(object id)
        {
            Venta au = _IVentaRepository.GetById(id);
            VentaResponse res = _Mapper.Map<VentaResponse>(au);
            return res;
        }

        public VentaResponse Update(VentaRequest entity)
        {
            Venta au = _Mapper.Map<Venta>(entity);
            au = _IVentaRepository.Update(au);
            VentaResponse res = _Mapper.Map<VentaResponse>(au);
            return res;
        }

        public List<VentaResponse> UpdateMultiple(List<VentaRequest> request)
        {
            List<Venta> au = _Mapper.Map<List<Venta>>(request);
            au = _IVentaRepository.UpdateMultiple(au);
            List<VentaResponse> res = _Mapper.Map<List<VentaResponse>>(au);
            return res;
        }
    }
}
