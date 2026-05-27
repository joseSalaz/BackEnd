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

    public class LibroAutorBussines : ILibroAutorBussines

    {
        #region Declaracion de vcariables generales
        public readonly ILibroAutorRepository _ILibroAutorRepository = null;
        public readonly IMapper _Mapper;

        
        #endregion

        #region constructor 
        public LibroAutorBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _ILibroAutorRepository = new LibroAutorRepository();
        }
        #endregion

        public LibroAutorResponse Create(LibroAutorRequest entity)
        {
            LibroAutor au = _Mapper.Map<LibroAutor>(entity);
            au = _ILibroAutorRepository.Create(au);
            LibroAutorResponse res = _Mapper.Map<LibroAutorResponse>(au);
            return res;
        }

        public List<LibroAutorResponse> CreateMultiple(List<LibroAutorRequest> request)
        {
            List<LibroAutor> au = _Mapper.Map<List<LibroAutor>>(request);
            au = _ILibroAutorRepository.InsertMultiple(au);
            List<LibroAutorResponse> res = _Mapper.Map<List<LibroAutorResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ILibroAutorRepository.Delete(id);
        }

        public int deleteMultipleItems(List<LibroAutorRequest> request)
        {
            List<LibroAutor> au = _Mapper.Map<List<LibroAutor>>(request);
            int cantidad = _ILibroAutorRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<LibroAutorResponse> getAll()
        {
            List<LibroAutor> lsl = _ILibroAutorRepository.GetAll();
            List<LibroAutorResponse> res = _Mapper.Map<List<LibroAutorResponse>>(lsl);
            return res;
        }

        public List<LibroAutorResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Autor>> GetAutoresByLibroId(int libroId)
        {
            var autores = await _ILibroAutorRepository.GetAutoresByLibroId(libroId);
            return autores;
        }

        public async Task<List<Libro>> GetLibrosByAutorId(int autorId)
        {
            var libros = await _ILibroAutorRepository.GetLibrosByAutorId(autorId);
            return libros;
        }

        public LibroAutorResponse getById(object id)
        {
            LibroAutor au = _ILibroAutorRepository.GetById(id);
            LibroAutorResponse res = _Mapper.Map<LibroAutorResponse>(au);
            return res;
        }

      

        public LibroAutorResponse Update(LibroAutorRequest entity)
        {
            LibroAutor au = _Mapper.Map<LibroAutor>(entity);
            au = _ILibroAutorRepository.Update(au);
            LibroAutorResponse res = _Mapper.Map<LibroAutorResponse>(au);
            return res;
        }

        public List<LibroAutorResponse> UpdateMultiple(List<LibroAutorRequest> request)
        {
            List<LibroAutor> au = _Mapper.Map<List<LibroAutor>>(request);
            au = _ILibroAutorRepository.UpdateMultiple(au);
            List<LibroAutorResponse> res = _Mapper.Map<List<LibroAutorResponse>>(au);
            return res;
        }
    }
}
