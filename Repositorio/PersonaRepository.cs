using DBModel.DB;
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        public List<Persona> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public Persona GetByTipoNroDocumento(string tipoDocumento, string NroDocumento)
        {
            Persona vPersona = new Persona();
            //tipoDocumento ==> RUC | DNI

            int tDocumento = 0;

            switch (tipoDocumento.ToLower())
            {
                case "dni": tDocumento = 1; break;
                case "ruc": tDocumento = 2; break;
                default:
                    return vPersona;
            }


#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            vPersona = db.Personas
                .Where(x => x.TipoDocumento == tDocumento)
                .Where(x => x.NumeroDocumento == NroDocumento)
                .FirstOrDefault()
                ;
            return vPersona;
        }
    }
}
