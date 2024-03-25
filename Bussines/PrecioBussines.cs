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
    public class PrecioBussines : IPrecioBussines
    {
        #region Declaracion de vcariables generales
        public readonly IPrecioRepository _IPrecioRepository = null;
        public readonly IMapper _Mapper;

        public PrecioBussines()
        {
        }
        #endregion

        #region constructor 
        public PrecioBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IPrecioRepository = new PrecioRepository();
        }
        #endregion

        public PrecioResponse Create(PrecioRequest entity)
        {
            Precio au = _Mapper.Map<Precio>(entity);
            au = _IPrecioRepository.Create(au);
            PrecioResponse res = _Mapper.Map<PrecioResponse>(au);
            return res;
        }

        public List<PrecioResponse> CreateMultiple(List<PrecioRequest> request)
        {
            List<Precio> au = _Mapper.Map<List<Precio>>(request);
            au = _IPrecioRepository.InsertMultiple(au);
            List<PrecioResponse> res = _Mapper.Map<List<PrecioResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IPrecioRepository.Delete(id);
        }

        public int deleteMultipleItems(List<PrecioRequest> request)
        {
            List<Precio> au = _Mapper.Map<List<Precio>>(request);
            int cantidad = _IPrecioRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PrecioResponse> getAll()
        {
            List<Precio> lsl = _IPrecioRepository.GetAll();
            List<PrecioResponse> res = _Mapper.Map<List<PrecioResponse>>(lsl);
            return res;
        }

        public List<PrecioResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PrecioResponse getById(object id)
        {
            Precio au = _IPrecioRepository.GetById(id);
            PrecioResponse res = _Mapper.Map<PrecioResponse>(au);
            return res;
        }

        public PrecioResponse Update(PrecioRequest entity)
        {
            Precio au = _Mapper.Map<Precio>(entity);
            au = _IPrecioRepository.Update(au);
            PrecioResponse res = _Mapper.Map<PrecioResponse>(au);
            return res;
        }

        public List<PrecioResponse> UpdateMultiple(List<PrecioRequest> request)
        {
            List<Precio> au = _Mapper.Map<List<Precio>>(request);
            au = _IPrecioRepository.UpdateMultiple(au);
            List<PrecioResponse> res = _Mapper.Map<List<PrecioResponse>>(au);
            return res;
        }
        public async Task<Libro> ObtenerLibroPorPrecioId(int precioId)
        {
            return await _IPrecioRepository.GetLibroByPrecioId(precioId);
        }
    }
}
