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
    public class DocEntradaBussines : IDocEntradaBussines
    {
        #region Declaracion de vcariables generales
        public readonly IDocEntradaRepository _IDocEntradaRepository = null;
        public readonly IMapper _Mapper;

        public DocEntradaBussines()
        {
        }
        #endregion

        #region constructor 
        public DocEntradaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IDocEntradaRepository = new DocEntradaRepository();
        }
        #endregion

        public DocEntradaResponse Create(DocEntradaRequest entity)
        {
            DocEntrada au = _Mapper.Map<DocEntrada>(entity);
            au = _IDocEntradaRepository.Create(au);
            DocEntradaResponse res = _Mapper.Map<DocEntradaResponse>(au);
            return res;
        }

        public List<DocEntradaResponse> CreateMultiple(List<DocEntradaRequest> request)
        {
            List<DocEntrada> au = _Mapper.Map<List<DocEntrada>>(request);
            au = _IDocEntradaRepository.InsertMultiple(au);
            List<DocEntradaResponse> res = _Mapper.Map<List<DocEntradaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IDocEntradaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<DocEntradaRequest> request)
        {
            List<DocEntrada> au = _Mapper.Map<List<DocEntrada>>(request);
            int cantidad = _IDocEntradaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<DocEntradaResponse> getAll()
        {
            List<DocEntrada> lsl = _IDocEntradaRepository.GetAll();
            List<DocEntradaResponse> res = _Mapper.Map<List<DocEntradaResponse>>(lsl);
            return res;
        }

        public List<DocEntradaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public DocEntradaResponse getById(object id)
        {
            DocEntrada au = _IDocEntradaRepository.GetById(id);
            DocEntradaResponse res = _Mapper.Map<DocEntradaResponse>(au);
            return res;
        }

        public DocEntradaResponse Update(DocEntradaRequest entity)
        {
            DocEntrada au = _Mapper.Map<DocEntrada>(entity);
            au = _IDocEntradaRepository.Update(au);
            DocEntradaResponse res = _Mapper.Map<DocEntradaResponse>(au);
            return res;
        }

        public List<DocEntradaResponse> UpdateMultiple(List<DocEntradaRequest> request)
        {
            List<DocEntrada> au = _Mapper.Map<List<DocEntrada>>(request);
            au = _IDocEntradaRepository.UpdateMultiple(au);
            List<DocEntradaResponse> res = _Mapper.Map<List<DocEntradaResponse>>(au);
            return res;
        }
    }
}
