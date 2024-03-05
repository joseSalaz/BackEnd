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
    public class LibroGeneroBussines : ILibroGeneroBussines
    {
        #region Declaracion de vcariables generales
        public readonly ILibroGeneroRepository _ILibroGeneroRepository = null;
        public readonly IMapper _Mapper;
        #endregion

        #region constructor 
        public LibroGeneroBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ILibroGeneroRepository = new LibroGeneroRepository();
        }
        #endregion

        public LibroGeneroResponse Create(LibroGeneroRequest entity)
        {
            LibroGenero au = _Mapper.Map<LibroGenero>(entity);
            au = _ILibroGeneroRepository.Create(au);
            LibroGeneroResponse res = _Mapper.Map<LibroGeneroResponse>(au);
            return res;
        }

        public List<LibroGeneroResponse> CreateMultiple(List<LibroGeneroRequest> request)
        {
            List<LibroGenero> au = _Mapper.Map<List<LibroGenero>>(request);
            au = _ILibroGeneroRepository.InsertMultiple(au);
            List<LibroGeneroResponse> res = _Mapper.Map<List<LibroGeneroResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ILibroGeneroRepository.Delete(id);
        }

        public int deleteMultipleItems(List<LibroGeneroRequest> request)
        {
            List<LibroGenero> au = _Mapper.Map<List<LibroGenero>>(request);
            int cantidad = _ILibroGeneroRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<LibroGeneroResponse> getAll()
        {
            List<LibroGenero> lsl = _ILibroGeneroRepository.GetAll();
            List<LibroGeneroResponse> res = _Mapper.Map<List<LibroGeneroResponse>>(lsl);
            return res;
        }

        public List<LibroGeneroResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public LibroGeneroResponse getById(object id)
        {
            LibroGenero au = _ILibroGeneroRepository.GetById(id);
            LibroGeneroResponse res = _Mapper.Map<LibroGeneroResponse>(au);
            return res;
        }

        public LibroGeneroResponse Update(LibroGeneroRequest entity)
        {
            LibroGenero au = _Mapper.Map<LibroGenero>(entity);
            au = _ILibroGeneroRepository.Update(au);
            LibroGeneroResponse res = _Mapper.Map<LibroGeneroResponse>(au);
            return res;
        }

        public List<LibroGeneroResponse> UpdateMultiple(List<LibroGeneroRequest> request)
        {
            List<LibroGenero> au = _Mapper.Map<List<LibroGenero>>(request);
            au = _ILibroGeneroRepository.UpdateMultiple(au);
            List<LibroGeneroResponse> res = _Mapper.Map<List<LibroGeneroResponse>>(au);
            return res;
        }
    }
}
