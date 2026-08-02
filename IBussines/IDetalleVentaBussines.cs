<<<<<<< HEAD
using DBModel.DB;
=======
﻿using DBModel.DB;
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
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
<<<<<<< HEAD
  public interface IDetalleVentaBussines : ICRUDBussnies<DetalleVentaRequest, DetalleVentaResponse>
  {
    Task<IEnumerable<DetalleVenta>> GetDetalleVentasByPersonaId(int idPersona);
    Task<IEnumerable<DetalleVenta>> GetDetalleVentasByVentaId(int idVenta);
    Task<bool> UpdateEstadoPedidosAndCreateImagenes(int idVenta, EstadoPedidoRequest request, List<IFormFile> images);
    Task<EstadoPedido> GetEstadoPedidoByDetalleVentaIdAsync(int idDetalleVenta);

    Task<List<ProductosMasVendidosResponse>> ObtenerProductosMasVendidosDelMesAsync(int mes, int anio);
  }
=======
    public interface IDetalleVentaBussines : ICRUDBussnies<DetalleVentaRequest,DetalleVentaResponse>
    {
        Task<IEnumerable<DetalleVenta>> GetDetalleVentasByPersonaId(int idPersona);
        Task<IEnumerable<DetalleVenta>> GetDetalleVentasByVentaId(int idVenta);
        Task<bool> UpdateEstadoPedidosAndCreateImagenes(int idVenta, EstadoPedidoRequest request, List<IFormFile> images);
        Task<EstadoPedido> GetEstadoPedidoByDetalleVentaIdAsync(int idDetalleVenta);

        Task<List<ProductosMasVendidosResponse>> ObtenerProductosMasVendidosDelMesAsync(int mes, int anio);
    }
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
}
