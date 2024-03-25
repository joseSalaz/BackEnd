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
    public interface IPrecioBussines : ICRUDBussnies<PrecioRequest,PrecioResponse>
    {
        public Task<Libro> ObtenerLibroPorPrecioId(int precioId);
    }
}
