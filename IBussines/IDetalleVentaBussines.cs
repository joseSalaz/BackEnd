using DBModel.DB;
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
    public interface IDetalleVentaBussines : ICRUDBussnies<DetalleVentaRequest,DetalleVentaResponse>
    {
        Task<IEnumerable<DetalleVenta>> GetDetalleVentasByPersonaId(int idPersona);
        Task<IEnumerable<DetalleVenta>> GetDetalleVentasByVentaId(int idVenta);
        Task<bool> UpdateEstadoPedidosAndCreateImagenes(int idVenta, EstadoPedidoRequest request, List<IFormFile> images);
        Task<EstadoPedido> GetEstadoPedidoByDetalleVentaIdAsync(int idDetalleVenta);


    }
}
