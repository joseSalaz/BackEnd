using DBModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IRepository
{
    public interface IDireccionRepository:ICRUDRepositorio<Direccion>
    {
        Task<List<Direccion>> GetDireccionesPorUsuario(int idPersona);
        void SetDireccionPredeterminada(int idDireccion);
        bool ExisteDireccionEnVenta(int idDireccion);
    }
}
