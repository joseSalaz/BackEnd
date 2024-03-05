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

    public class GeneroBussines : IGeneroBussines

    {
        #region Declaracion de vcariables generales
        public readonly IGeneroRepository _IGeneroRepository = null;
        public readonly IMapper _Mapper;

        public GeneroBussines()
        {
        }
        #endregion

        #region constructor 
        public GeneroBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IGeneroRepository = new GeneroRepository();
        }
        #endregion

        public GeneroResponse Create(GeneroRequest entity)
        {
            Genero au = _Mapper.Map<Genero>(entity);
            au = _IGeneroRepository.Create(au);
            GeneroResponse res = _Mapper.Map<GeneroResponse>(au);
            return res;
        }

        public List<GeneroResponse> CreateMultiple(List<GeneroRequest> request)
        {
            List<Genero> au = _Mapper.Map<List<Genero>>(request);
            au = _IGeneroRepository.InsertMultiple(au);
            List<GeneroResponse> res = _Mapper.Map<List<GeneroResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IGeneroRepository.Delete(id);
        }

        public int deleteMultipleItems(List<GeneroRequest> request)
        {
            List<Genero> au = _Mapper.Map<List<Genero>>(request);
            int cantidad = _IGeneroRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<GeneroResponse> getAll()
        {
            List<Genero> lsl = _IGeneroRepository.GetAll();
            List<GeneroResponse> res = _Mapper.Map<List<GeneroResponse>>(lsl);
            return res;
        }

        public List<GeneroResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public GeneroResponse getById(object id)
        {
            Genero au = _IGeneroRepository.GetById(id);
            GeneroResponse res = _Mapper.Map<GeneroResponse>(au);
            return res;
        }

        public GeneroResponse Update(GeneroRequest entity)
        {
            Genero au = _Mapper.Map<Genero>(entity);
            au = _IGeneroRepository.Update(au);
            GeneroResponse res = _Mapper.Map<GeneroResponse>(au);
            return res;
        }

        public List<GeneroResponse> UpdateMultiple(List<GeneroRequest> request)
        {
            List<Genero> au = _Mapper.Map<List<Genero>>(request);
            au = _IGeneroRepository.UpdateMultiple(au);
            List<GeneroResponse> res = _Mapper.Map<List<GeneroResponse>>(au);
            return res;
        }
    }
}
