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
    public class DocSalidaBussines : IDocSalidaBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDocSalidaRepository _IDocSalidaRepository = null;
        public readonly IMapper _Mapper;

        public DocSalidaBussines()
        {
        }
        #endregion

        #region constructor 
        public DocSalidaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDocSalidaRepository = new DocSalidaRepository();
        }
        #endregion

        public DocSalidaResponse Create(DocSalidaRequest entity)
        {
            DocSalida au = _Mapper.Map<DocSalida>(entity);
            au = _IDocSalidaRepository.Create(au);
            DocSalidaResponse res = _Mapper.Map<DocSalidaResponse>(au);
            return res;
        }

        public List<DocSalidaResponse> CreateMultiple(List<DocSalidaRequest> request)
        {
            List<DocSalida> au = _Mapper.Map<List<DocSalida>>(request);
            au = _IDocSalidaRepository.InsertMultiple(au);
            List<DocSalidaResponse> res = _Mapper.Map<List<DocSalidaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDocSalidaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DocSalidaRequest> request)
        {
            List<DocSalida> au = _Mapper.Map<List<DocSalida>>(request);
            int cantidad = _IDocSalidaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DocSalidaResponse> getAll()
        {
            List<DocSalida> lsl = _IDocSalidaRepository.GetAll();
            List<DocSalidaResponse> res = _Mapper.Map<List<DocSalidaResponse>>(lsl);
            return res;
        }

        public List<DocSalidaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DocSalidaResponse getById(object id)
        {
            DocSalida au = _IDocSalidaRepository.GetById(id);
            DocSalidaResponse res = _Mapper.Map<DocSalidaResponse>(au);
            return res;
        }

        public DocSalidaResponse Update(DocSalidaRequest entity)
        {
            DocSalida au = _Mapper.Map<DocSalida>(entity);
            au = _IDocSalidaRepository.Update(au);
            DocSalidaResponse res = _Mapper.Map<DocSalidaResponse>(au);
            return res;
        }

        public List<DocSalidaResponse> UpdateMultiple(List<DocSalidaRequest> request)
        {
            List<DocSalida> au = _Mapper.Map<List<DocSalida>>(request);
            au = _IDocSalidaRepository.UpdateMultiple(au);
            List<DocSalidaResponse> res = _Mapper.Map<List<DocSalidaResponse>>(au);
            return res;
        }
    }
}
