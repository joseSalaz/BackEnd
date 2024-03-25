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
    public class SubcategoriaBussines : ISubcategoriaBussines
    {
        #region Declaracion de vcariables generales
        public readonly ISubcategoriaRepository _ISubcategoriaRepository = null;
        public readonly IMapper _Mapper;

        #endregion

        #region constructor 
        public SubcategoriaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ISubcategoriaRepository = new SubcategoriaRepository();
        }
        #endregion

        public SubcategoriaResponse Create(SubcategoriaRequest entity)
        {
            Subcategoria au = _Mapper.Map<Subcategoria>(entity);
            au = _ISubcategoriaRepository.Create(au);
            SubcategoriaResponse res = _Mapper.Map<SubcategoriaResponse>(au);
            return res;
        }

        public List<SubcategoriaResponse> CreateMultiple(List<SubcategoriaRequest> request)
        {
            List<Subcategoria> au = _Mapper.Map<List<Subcategoria>>(request);
            au = _ISubcategoriaRepository.InsertMultiple(au);
            List<SubcategoriaResponse> res = _Mapper.Map<List<SubcategoriaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ISubcategoriaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<SubcategoriaRequest> request)
        {
            List<Subcategoria> au = _Mapper.Map<List<Subcategoria>>(request);
            int cantidad = _ISubcategoriaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<SubcategoriaResponse> getAll()
        {
            List<Subcategoria> lsl = _ISubcategoriaRepository.GetAll();
            List<SubcategoriaResponse> res = _Mapper.Map<List<SubcategoriaResponse>>(lsl);
            return res;
        }

        public List<SubcategoriaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public SubcategoriaResponse getById(object id)
        {
            Subcategoria au = _ISubcategoriaRepository.GetById(id);
            SubcategoriaResponse res = _Mapper.Map<SubcategoriaResponse>(au);
            return res;
        }

        public async Task<List<int>> GetLibrosIdsBySubcategoria(int subcategoriaId)
        {
            return await _ISubcategoriaRepository.GetLibroIdsBySubcategoria(subcategoriaId);
        }


        public SubcategoriaResponse Update(SubcategoriaRequest entity)
        {
            Subcategoria au = _Mapper.Map<Subcategoria>(entity);
            au = _ISubcategoriaRepository.Update(au);
            SubcategoriaResponse res = _Mapper.Map<SubcategoriaResponse>(au);
            return res;
        }

        public List<SubcategoriaResponse> UpdateMultiple(List<SubcategoriaRequest> request)
        {
            List<Subcategoria> au = _Mapper.Map<List<Subcategoria>>(request);
            au = _ISubcategoriaRepository.UpdateMultiple(au);
            List<SubcategoriaResponse> res = _Mapper.Map<List<SubcategoriaResponse>>(au);
            return res;
        }
    }
}
