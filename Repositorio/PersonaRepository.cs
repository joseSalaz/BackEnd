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
        //public Persona buscarporDNI(string DNI)
        //{
        //    Persona person = db.Personas.Where(x => x.NumeroDocumento == DNI).FirstOrDefault();
        //    return person;
        //}

        public List<Persona> GetAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public Persona GetByTipoNroDocumento(string TipoDocumento, string NumeroDocumento)
        {
            if (string.IsNullOrEmpty(TipoDocumento) || string.IsNullOrEmpty(NumeroDocumento))
            {
               
                return new Persona();
            }

            Persona vPersona = new Persona();
            //tipoDocumento ==> RUC | DNI

            int tDocumento = 0;

            switch (TipoDocumento.ToLower())
            {
                case "dni":
                    tDocumento = 1;
                    break;
                case "ruc":
                    tDocumento = 2;
                    break;
                default:
                    return vPersona;
            }
            vPersona = db.Personas
                .Where(x => x.TipoDocumento == TipoDocumento && x.NumeroDocumento == NumeroDocumento)
                .FirstOrDefault();

            return vPersona;
        }

        public Persona GetByIdSub(string sub)
        {
            if (string.IsNullOrEmpty(sub))
            {
                return null;
            }
            Persona persona = db.Personas
                                .FirstOrDefault(p => p.Sub == sub);
            return persona;
        }
    }
}
