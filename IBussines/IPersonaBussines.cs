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
    public interface IPersonaBussines : ICRUDBussnies<PersonaRequest, PersonaResponse>
    {
        //PersonaResponse BuscarporDNI(string dni);
        Persona GetByTipoNroDocumento(string TipoDocumento, string NumeroDocumento);
        PersonaResponse GetByIdSub(string sub);
        Persona GetPersonaByDocumento(string documento);
        Task<(List<PersonaResponse>, int)> GetPersonaPaginados(int page, int pageSize);


    }
}
