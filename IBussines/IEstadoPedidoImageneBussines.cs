using Microsoft.AspNetCore.Http;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface IEstadoPedidoImageneBussines: ICRUDBussnies<EstadoPedidoImageneRequest, EstadoPedidoImageneResponse>
    {

        Task<EstadoPedidoImageneResponse> CreateWithImagesAsync(EstadoPedidoImageneRequest entity, List<IFormFile> images);
        Task<List<EstadoPedidoImageneResponse>> CreateMultipleWithImagesAsync(List<EstadoPedidoImageneRequest> requests, List<List<IFormFile>> imagesList);
    }
}
