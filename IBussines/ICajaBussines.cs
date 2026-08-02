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
  public interface ICajaBussines : ICRUDBussnies<CajaRequest, CajaResponse>
  {
    Caja RegistrarVentaEnCajaDelDia();
    Caja RegistrarventasEcomerce();
<<<<<<< HEAD
    Caja ObtenerCajaPorId(int id);
=======
>>>>>>> 2547f9ea75729e66eae6c655c2747dcbd77035c4
  }
}
