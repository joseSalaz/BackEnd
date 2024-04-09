using DBModel.DB;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface IVentaBussines:ICRUDBussnies<VentaRequest, VentaResponse>
    {
        Task<List<DetalleVenta>> GetDetalleVentaByVentaId(int idVenta);
        Task<MemoryStream> CreateVentaPdf(int idVenta);
    }
}
