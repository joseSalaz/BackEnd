using AutoMapper;
using Constantes;
using DBModel.DB;

using IBussines;
using IRepositorio;
using IRepository;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class LibroBussines : ILibroBussines
    {
        #region Declaracion de vcariables generales
        
        public readonly ILibroRepository _ILibroRepository = null;
        public readonly IMapper _Mapper;
        private readonly IAzureStorage _azureStorage;

        #endregion

        #region constructor 
        public LibroBussines(IMapper mapper, IAzureStorage azureStorage)
        {
            _Mapper = mapper;
            _ILibroRepository = new LibroRepository();
            _azureStorage = azureStorage;

        }
        #endregion

        public LibroResponse Create(LibroRequest entity)
        {
            Libro au = _Mapper.Map<Libro>(entity);
            au = _ILibroRepository.Create(au);
            LibroResponse res = _Mapper.Map<LibroResponse>(au);
            return res;
        }

        public List<LibroResponse> CreateMultiple(List<LibroRequest> request)
        {
            List<Libro> au = _Mapper.Map<List<Libro>>(request);
            au = _ILibroRepository.InsertMultiple(au);
            List<LibroResponse> res = _Mapper.Map<List<LibroResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _ILibroRepository.Delete(id);
        }

        public int deleteMultipleItems(List<LibroRequest> request)
        {
            List<Libro> au = _Mapper.Map<List<Libro>>(request);
            int cantidad = _ILibroRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<LibroResponse> getAll()
        {
            List<Libro> lsl = _ILibroRepository.GetAll();
            List<LibroResponse> res = _Mapper.Map<List<LibroResponse>>(lsl);
            return res;
        }

        public List<LibroResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public LibroResponse getById(object id)
        {
            Libro au = _ILibroRepository.GetById(id);
            LibroResponse res = _Mapper.Map<LibroResponse>(au);
            return res;
        }

        public LibroResponse Update(LibroRequest entity)
        {
            Libro au = _Mapper.Map<Libro>(entity);
            au = _ILibroRepository.Update(au);
            LibroResponse res = _Mapper.Map<LibroResponse>(au);
            return res;
        }

        public List<LibroResponse> UpdateMultiple(List<LibroRequest> request)
        {
            List<Libro> au = _Mapper.Map<List<Libro>>(request);
            au = _ILibroRepository.UpdateMultiple(au);
            List<LibroResponse> res = _Mapper.Map<List<LibroResponse>>(au);
            return res;
        }

        public async Task<LibroResponse> CreateWithImage(LibroRequest entity, IFormFile imageFile)
        {
            // Mapear la entidad de solicitud a la entidad de modelo
            Libro libro = _Mapper.Map<Libro>(entity);

            // Guardar la imagen en Azure Blob Storage y obtener su URL
            string imageUrl = await _azureStorage.SaveFile("imagenes", imageFile);

            // Asignar la URL de la imagen al campo correspondiente en la entidad
            libro.Imagen = imageUrl;

            // Crear el libro en la base de datos
            libro = _ILibroRepository.Create(libro);

            // Mapear el libro creado a la respuesta y devolverlo
            return _Mapper.Map<LibroResponse>(libro);
        }

        public async Task<List<Libro>> GetLibrosByIds(List<int> ids)
        {
            return await _ILibroRepository.GetByIds(ids);
        }

        public async Task<Libro> ObtenerLibroConPreciosYPublicoObjetivo(int libroId)
        {
            return await _ILibroRepository.GetLibroConPreciosYPublicoObjetivo(libroId);
        }
        public async Task<Libro> ObtenerLibroCompletoPorIds(Libro libroConIds)
        {
            var libroCompleto = await _ILibroRepository.GetLibroConPreciosYPublicoObjetivo(libroConIds.IdLibro);
            if (libroCompleto == null)
            {
                return null;
            }
            foreach (var precio in libroCompleto.Precios)
            {

                var precioCorrespondiente = libroConIds.Precios.FirstOrDefault(p => p.IdPrecios == precio.IdPrecios);
                if (precioCorrespondiente != null)
                {
                    precio.PrecioVenta = precioCorrespondiente.PrecioVenta;
                    precio.PorcUtilidad = precioCorrespondiente.PorcUtilidad;
                }
            }
            return libroCompleto;
        }

        public async Task<List<Precio>> GetPreciosByLibroId(int libroId)
        {
            // Obtener precios del repositorio
            var precios = await _ILibroRepository.GetPreciosByLibroId(libroId);

            // Devolver la lista de precios obtenida del repositorio
            return precios;
        }


        public async Task<Kardex> GetKardexByLibroId(int libroId)
        {
            return await _ILibroRepository.GetKardexByLibroId(libroId);
        }

        public async Task<(List<LibroResponse>, int)> GetLibrosPaginados(int page, int pageSize)
        {
            var (libros, totalItems) = await _ILibroRepository.GetLibrosPaginados(page, pageSize);
            var libroResponses = _Mapper.Map<List<LibroResponse>>(libros);
            return (libroResponses, totalItems);
        }


        public async Task<List<LibroResponse>> filtroComplete(string query)
        {
            var libros = await _ILibroRepository.filtroComplete(query);
            return _Mapper.Map<List<LibroResponse>>(libros);
        }

    }

}

