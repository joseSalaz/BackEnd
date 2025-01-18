using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepository;
using IService;
using Microsoft.AspNetCore.Http;
using Models.RequestResponse;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class EstadoPedidoImageneBussines : IEstadoPedidoImageneBussines
    {
        #region Declaracion de vcariables generales
        public readonly IEstadoPedidoImageneRepository _IEstadoPedidoImageneRepository = null;
        public readonly IMapper _Mapper;
        public readonly IFirebaseStorageService _firebaseStorageService;
        #endregion

        #region constructor 
        public EstadoPedidoImageneBussines(IMapper mapper,IFirebaseStorageService firebaseStorage)
        {
            _Mapper = mapper;
            _IEstadoPedidoImageneRepository = new EstadoPedidoImageneRepository();
            _firebaseStorageService = firebaseStorage;
        }
        #endregion

        public EstadoPedidoImageneResponse Create(EstadoPedidoImageneRequest entity)
        {
            EstadoPedidoImagene au = _Mapper.Map<EstadoPedidoImagene>(entity);
            au = _IEstadoPedidoImageneRepository.Create(au);
            EstadoPedidoImageneResponse res = _Mapper.Map<EstadoPedidoImageneResponse>(au);
            return res;
        }

        public List<EstadoPedidoImageneResponse> CreateMultiple(List<EstadoPedidoImageneRequest> request)
        {
            List<EstadoPedidoImagene> au = _Mapper.Map<List<EstadoPedidoImagene>>(request);
            au = _IEstadoPedidoImageneRepository.InsertMultiple(au);
            List<EstadoPedidoImageneResponse> res = _Mapper.Map<List<EstadoPedidoImageneResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IEstadoPedidoImageneRepository.Delete(id);
        }

        public int deleteMultipleItems(List<EstadoPedidoImageneRequest> request)
        {
            List<EstadoPedidoImagene> au = _Mapper.Map<List<EstadoPedidoImagene>>(request);
            int cantidad = _IEstadoPedidoImageneRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<EstadoPedidoImageneResponse> getAll()
        {
            List<EstadoPedidoImagene> lsl = _IEstadoPedidoImageneRepository.GetAll();
            List<EstadoPedidoImageneResponse> res = _Mapper.Map<List<EstadoPedidoImageneResponse>>(lsl);
            return res;
        }

        public List<EstadoPedidoImageneResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public EstadoPedidoImageneResponse getById(object id)
        {
            EstadoPedidoImagene au = _IEstadoPedidoImageneRepository.GetById(id);
            EstadoPedidoImageneResponse res = _Mapper.Map<EstadoPedidoImageneResponse>(au);
            return res;
        }

        public EstadoPedidoImageneResponse Update(EstadoPedidoImageneRequest entity)
        {
            EstadoPedidoImagene au = _Mapper.Map<EstadoPedidoImagene>(entity);
            au = _IEstadoPedidoImageneRepository.Update(au);
            EstadoPedidoImageneResponse res = _Mapper.Map<EstadoPedidoImageneResponse>(au);
            return res;
        }

        public List<EstadoPedidoImageneResponse> UpdateMultiple(List<EstadoPedidoImageneRequest> request)
        {
            List<EstadoPedidoImagene> au = _Mapper.Map<List<EstadoPedidoImagene>>(request);
            au = _IEstadoPedidoImageneRepository.UpdateMultiple(au);
            List<EstadoPedidoImageneResponse> res = _Mapper.Map<List<EstadoPedidoImageneResponse>>(au);
            return res;
        }


        public async Task<EstadoPedidoImageneResponse> CreateWithImagesAsync(EstadoPedidoImageneRequest entity, List<IFormFile> images)
        {
            // Convertir EstadoPedidoImageneRequest a EstadoPedidoImagene
            EstadoPedidoImagene pedido = _Mapper.Map<EstadoPedidoImagene>(entity);

            // Subir imágenes a Firebase y obtener las URLs
            var imageUrls = new List<string>();
            foreach (var image in images)
            {
                var url = await _firebaseStorageService.UploadPedidosImageAsync(image);
                imageUrls.Add(url);
            }

            // Asignar las URLs concatenadas al pedido
            pedido.UrlImagen = string.Join(",", imageUrls);

            // Verificar los datos antes de la inserción
            if (string.IsNullOrEmpty(pedido.UrlImagen))
            {
                throw new Exception("Las URLs de las imágenes no se generaron correctamente.");
            }

            // Guardar en el repositorio
            var createdPedido = _IEstadoPedidoImageneRepository.Create(pedido);

            // Verificar si se creó correctamente
            if (createdPedido == null)
            {
                throw new Exception("No se pudo guardar el EstadoPedidoImagene.");
            }

            // Convertir la entidad guardada en la respuesta
            EstadoPedidoImageneResponse response = _Mapper.Map<EstadoPedidoImageneResponse>(createdPedido);

            return response;
        }




        public async Task<List<EstadoPedidoImageneResponse>> CreateMultipleWithImagesAsync(List<EstadoPedidoImageneRequest> requests, List<List<IFormFile>> imagesList)
        {
            if (requests == null || requests.Count == 0)
                throw new ArgumentException("No se proporcionaron pedidos.");
            if (imagesList == null || requests.Count != imagesList.Count)
                throw new ArgumentException("El número de listas de imágenes no coincide con los pedidos.");

            var responses = new List<EstadoPedidoImageneResponse>();

            for (int i = 0; i < requests.Count; i++)
            {
                var currentRequest = requests[i];
                var currentImages = imagesList[i];

                foreach (var image in currentImages)
                {
                    // Subir la imagen a Firebase y obtener la URL
                    var url = await _firebaseStorageService.UploadPedidosImageAsync(image);

                    // Crear un registro para cada imagen
                    var pedidoImagen = new EstadoPedidoImagene
                    {
                        IdEstadoPedido = currentRequest.IdEstadoPedido,
                        UrlImagen = url,
                        Estado = currentRequest.Estado,
                        Fecha = currentRequest.Fecha,
                    };

                    // Guardar en la base de datos
                    var savedPedidoImagen = _IEstadoPedidoImageneRepository.Create(pedidoImagen);

                    // Mapear la respuesta y agregar a la lista
                    responses.Add(_Mapper.Map<EstadoPedidoImageneResponse>(savedPedidoImagen));
                }
            }

            return responses;
        }

    }
}
