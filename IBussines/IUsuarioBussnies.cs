using Models.RequestRequest;
using Models.RequestResponse;
using Models.ResponseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilInterface;

namespace IBussnies
{
    public interface IUsuarioBussnies : ICRUDBussnies<UsuarioRequest, UsuarioResponse>
    {
        UsuarioResponse GetByUserName(string userName);
    }
}
