using DBModel.DB;
using Microsoft.AspNetCore.Mvc;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussines
{
    public interface ICajaBussines : ICRUDBussnies <CajaRequest, CajaResponse>
    {
        Caja RegistrarVentaEnCajaDelDia();
    }
}
