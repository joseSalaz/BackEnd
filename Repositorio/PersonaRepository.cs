using DBModel.DB;
using IRepository;
using Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Repository
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        public Persona buscarporDNI(string DNI)
        {
            Persona person = db.Personas.Where(x => x.NumeroDocumento == DNI).FirstOrDefault();
            return person;
        }

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

        public Persona GetByDni(string documento)
        {
            if (string.IsNullOrEmpty(documento))
            {
                return null;
            }

            // Verifica la longitud del documento para determinar si es un DNI o un RUC
            if (documento.Length > 8) // Consideramos que más de 8 dígitos es un RUC
            {
                // Busca como RUC
                return db.Personas.FirstOrDefault(p => p.NumeroDocumento == documento && p.TipoDocumento == "RUC");
            }
            else
            {
                // Busca como DNI
                return db.Personas.FirstOrDefault(p => p.NumeroDocumento == documento && p.TipoDocumento == "DNI");
            }
        }

        public async Task<(List<Persona>, int)> GetPersonaPaginados(int page, int pageSize)
        {
            var query = dbSet.AsQueryable();
            int totalItems = await query.CountAsync();
            var persona = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (persona, totalItems);
        }


    }
}
