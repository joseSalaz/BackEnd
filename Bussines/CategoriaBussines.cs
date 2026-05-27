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
using UtilInterface;

namespace Bussines
{
    public class CategoriaBussines : ICategoriaBussines
    {
        
        #region Declaracion de vcariables generales
        public readonly ICategoriaRepository _ICategoriaRepository = null;
        public readonly IMapper _Mapper;

        public CategoriaBussines()
        {
        }
        #endregion

        #region constructor 
        public CategoriaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ICategoriaRepository = new CategoriaRepository();
        }
        #endregion

        public CategoriaResponse Create(CategoriaRequest entity)
        {
            Categoria au = _Mapper.Map<Categoria>(entity);
            au = _ICategoriaRepository.Create(au);
            CategoriaResponse res = _Mapper.Map<CategoriaResponse>(au);
            return res;
        }

        public List<CategoriaResponse> CreateMultiple(List<CategoriaRequest> request)
        {
            List<Categoria> au = _Mapper.Map<List<Categoria>>(request);
            au = _ICategoriaRepository.InsertMultiple(au);
            List<CategoriaResponse> res = _Mapper.Map<List<CategoriaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ICategoriaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<CategoriaRequest> request)
        {
            List<Categoria> au = _Mapper.Map<List<Categoria>>(request);
            int cantidad = _ICategoriaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<CategoriaResponse> getAll()
        {
            List<Categoria> lsl = _ICategoriaRepository.GetAll();
            List<CategoriaResponse> res = _Mapper.Map<List<CategoriaResponse>>(lsl);
            return res;
        }

        public List<CategoriaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public CategoriaResponse getById(object id)
        {
            Categoria au = _ICategoriaRepository.GetById(id);
            CategoriaResponse res = _Mapper.Map<CategoriaResponse>(au);
            return res;
        }

        public CategoriaResponse Update(CategoriaRequest entity)
        {
            Categoria au = _Mapper.Map<Categoria>(entity);
            au = _ICategoriaRepository.Update(au);
            CategoriaResponse res = _Mapper.Map<CategoriaResponse>(au);
            return res;
        }

        public List<CategoriaResponse> UpdateMultiple(List<CategoriaRequest> request)
        {
            List<Categoria> au = _Mapper.Map<List<Categoria>>(request);
            au = _ICategoriaRepository.UpdateMultiple(au);
            List<CategoriaResponse> res = _Mapper.Map<List<CategoriaResponse>>(au);
            return res;
        }
        public async Task<List<Libro>> GetLibrosByCategoriaId(int categoriaId)
        {
            return await _ICategoriaRepository.GetLibrosByCategoriaId(categoriaId);
        }

        public async Task<List<Subcategoria>> GetSubcategoriasByCategoriaId(int categoriaId)
        {
            return await _ICategoriaRepository.GetSubcategoriasByCategoriaId(categoriaId);
        }
    }
}

