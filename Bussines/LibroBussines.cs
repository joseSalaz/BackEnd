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
using Microsoft.IdentityModel.Tokens;
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
    
        private readonly IFirebaseStorageService _firebaseStorageService;
        private readonly IPrecioRepository _PrecioRepository;
        private readonly IKardexRepository _KardexRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly ILibroAutorRepository _libroAutorRepository;


        #endregion

        #region constructor 
        public LibroBussines(IMapper mapper, IFirebaseStorageService firebaseStorageService, IPrecioRepository iPrecioRepository,IKardexRepository kardexRepository
            ,IAutorRepository autorRepository,
            ILibroAutorRepository libroAutorRepository)
        {
            _Mapper = mapper;
            _ILibroRepository = new LibroRepository();
            
            _firebaseStorageService = firebaseStorageService;
            _PrecioRepository = iPrecioRepository;
            _KardexRepository = kardexRepository;
            _autorRepository = autorRepository;
            _libroAutorRepository = libroAutorRepository;


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

        public async Task<LibroResponse> GetByIdAsync(object id)
        {
            var libro = await _ILibroRepository.GetByIdAsync(id); // Llamada asincrónica
            if (libro == null)
            {
                throw new Exception("Libro no encontrado");
            }

            LibroResponse res = _Mapper.Map<LibroResponse>(libro);
            return res;
        }

        public LibroResponse Update(LibroRequest entity)
        {
            Libro au = _Mapper.Map<Libro>(entity);
            au = _ILibroRepository.Update(au);
            LibroResponse res = _Mapper.Map<LibroResponse>(au);
            return res;
        }
        public async Task<(List<Libro>, int)> FiltrarLibrosAsync(bool? estado, string titulo, int page, int pageSize)
        {
            return await _ILibroRepository.FiltrarLibrosAsync(estado, titulo, page, pageSize);
        }


        public async Task<bool> CambiarEstadoLibro(int libroId)
        {
            return await _ILibroRepository.CambiarEstadoLibro(libroId);
        }
        public List<LibroResponse> UpdateMultiple(List<LibroRequest> request)
        {
            List<Libro> au = _Mapper.Map<List<Libro>>(request);
            au = _ILibroRepository.UpdateMultiple(au);
            List<LibroResponse> res = _Mapper.Map<List<LibroResponse>>(au);
            return res;
        }

        //public async Task<LibroResponse> CreateWithImage(LibroRequest entity, IFormFile imageFile)
        //{
        //    // Mapear la entidad de solicitud a la entidad de modelo
        //    Libro libro = _Mapper.Map<Libro>(entity);

        //    // Guardar la imagen en Azure Blob Storage y obtener su URL
        //    string imageUrl = await _azureStorage.SaveFile("imagenes", imageFile);

        //    // Asignar la URL de la imagen al campo correspondiente en la entidad
        //    libro.Imagen = imageUrl;

        //    // Crear el libro en la base de datos
        //    libro = _ILibroRepository.Create(libro);

        //    // Mapear el libro creado a la respuesta y devolverlo
        //    return _Mapper.Map<LibroResponse>(libro);
        //}


        public async Task<LibroResponse> CreateWithImageFirebase(LibroconautorRequest entity, IFormFile imageFile, decimal precioVenta, int stock)
        {
            // Mapear la entidad de solicitud a la entidad de modelo
            Libro libro = _Mapper.Map<Libro>(entity.Libro);

            // Guardar la imagen en Firebase Storage y obtener su URL
            string imageUrl = await _firebaseStorageService.UploadFileAsync(imageFile, "libros-images");

            // Asignar la URL de la imagen al campo correspondiente en la entidad
            libro.Imagen = imageUrl;

            // Crear el libro en la base de datos
            libro = _ILibroRepository.Create(libro);

            if (entity.Autor.Nombre != null)
            {
                // Buscar si el autor ya existe (ahora usando await)
                Autor autors = await _autorRepository.GetByName(entity.Autor.Nombre);
                if (autors == null)
                {
                    // Crear el autor si no existe
                    autors = new Autor
                    {
                        Nombre = entity.Autor.Nombre,
                        Apellido = entity.Autor.Apellido,
                        Codigo = entity.Autor.Codigo,
                        Descripcion = entity.Autor.Descripcion
                    };
                    autors = _autorRepository.Create(autors);
                }
                // Crear la relación en la tabla intermedia LibroAutor
                LibroAutor libroAutor = new LibroAutor
                {
                    IdLibro = libro.IdLibro,
                    IdAutor = autors.IdAutor
                };
                _libroAutorRepository.Create(libroAutor);
            }
            else
            {
                Autor autors = await _autorRepository.GetByIdAsync(entity.Autor.IdAutor);

                    if (!string.IsNullOrEmpty(entity.Autor.Nombre))
                    {
                        autors.Nombre = entity.Autor.Nombre;
                    }

                    if (!string.IsNullOrEmpty(entity.Autor.Apellido))
                    {
                        autors.Apellido = entity.Autor.Apellido;
                    }

                    if (entity.Autor.Codigo != null)
                    {
                        autors.Codigo = entity.Autor.Codigo;
                    }

                    if (!string.IsNullOrEmpty(entity.Autor.Descripcion))
                    {
                        autors.Descripcion = entity.Autor.Descripcion;
                    }
                    _autorRepository.Update(autors);
                // Crear la relación en la tabla intermedia LibroAutor
                LibroAutor libroAutor = new LibroAutor
                {
                    IdLibro = libro.IdLibro,
                    IdAutor = autors.IdAutor
                };
                _libroAutorRepository.Create(libroAutor);
            }

            // Crear una instancia de Precios con los valores predeterminados
            Precio precios = new Precio
            {
                PrecioVenta = precioVenta,
                PorcUtilidad = null,
                IdLibro = libro.IdLibro,
                IdPublicoObjetivo = 1
            };

            // Guardar los precios en la base de datos
            _PrecioRepository.Create(precios);

            // Crear el registro de Kardex
            Kardex kardex = new Kardex
            {
                IdSucursal = 1,
                IdLibro = libro.IdLibro,
                CantidadSalida = 0,
                CantidadEntrada = 0,
                Stock = stock,
                UltPrecioCosto = 0,
            };
            _KardexRepository.Create(kardex);

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


        public async Task<LibroResponse> UpdateLib(LibroconautorRequest entity,IFormFile? imageFile, decimal precioVenta, int stock)
        {
            // Obtener el libro desde el repositorio
            var libro = await _ILibroRepository.GetByIdAsync(entity.Libro.IdLibro);
            Console.WriteLine($"Recibiendo ID: {entity.Libro.IdLibro}");
            if (libro == null)
            {
                throw new Exception("Libro no encontrado");
            }

            // Mapear los cambios desde el request hacia el libro
            libro.Titulo = entity.Libro.Titulo;
            libro.Isbn = entity.Libro.Isbn;
            libro.Tamanno = entity.Libro.Tamanno;
            libro.Descripcion = entity.Libro.Descripcion;
            libro.Condicion = entity.Libro.Condicion;
            libro.Impresion = entity.Libro.Impresion;
            libro.TipoTapa = entity.Libro.TipoTapa;
            libro.Estado = entity.Libro.Estado;
            
            // Actualizar el libro en la base de datos
            _ILibroRepository.Update(libro);
            // Manejar la imagen si se proporciona una nueva
            if (imageFile != null)
            {
                // Subir la nueva imagen a Firebase Storage
                string newImageUrl = await _firebaseStorageService.UploadFileAsync(imageFile, "libros");

                // Actualizar la URL de la imagen en el libro
                libro.Imagen = newImageUrl;
                _ILibroRepository.Update(libro);
            }

            // Actualizar el precio
            var precio = await _PrecioRepository.GetByIdAsync(libro.IdLibro);
            if (precio != null)
            {
                precio.PrecioVenta = precioVenta;
                _PrecioRepository.Update(precio);
            }
            else
            {
                // Si no existe un precio, crear uno nuevo
                Precio nuevoPrecio = new Precio
                {
                    PrecioVenta = precioVenta,
                    IdLibro = libro.IdLibro,
                    IdPublicoObjetivo = 1  // Si es necesario, ajustar
                };
                _PrecioRepository.Create(nuevoPrecio);
            }

            // Actualizar el stock
            var kardex = await _KardexRepository.GetByIdAsync(libro.IdLibro);
            if (kardex != null)
            {
                kardex.Stock = stock;
                _KardexRepository.Update(kardex);
            }
            else
            {
                // Si no existe un kardex, crear uno nuevo
                Kardex nuevoKardex = new Kardex
                {
                    IdLibro = libro.IdLibro,
                    Stock = stock,
                    IdSucursal = 1,  // Ajustar según sea necesario
                    CantidadEntrada = 0,
                    CantidadSalida = 0,
                    UltPrecioCosto = 0
                };
                _KardexRepository.Create(nuevoKardex);
            }
            // Mapear el libro actualizado a la respuesta
            return _Mapper.Map<LibroResponse>(libro);
        }

    }

}

