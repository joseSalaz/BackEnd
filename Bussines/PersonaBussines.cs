using AutoMapper;
using DBModel.DB;
using IBussines;
using IRepository;
using IService;
using Models.RequestResponse;
using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class PersonaBussines: IPersonaBussines
    {
        #region Declaracion de vcariables generales
        public readonly IPersonaRepository _IPersonaRepository = null;
        public readonly IMapper _Mapper;
        private readonly IApisPeruServices _apisPeruServices;
        private readonly IPersonaRepository _persona;
      

        public PersonaBussines()
        {
        }
        #endregion

        #region constructor 
        public PersonaBussines(IMapper mapper)
        {
            _Mapper = mapper;
            _IPersonaRepository = new PersonaRepository();
            _persona = new PersonaRepository();
            _apisPeruServices = new ApisPeruServices();
        }
        #endregion

        public PersonaResponse Create(PersonaRequest entity)
        {
            Persona au = _Mapper.Map<Persona>(entity);
            au = _IPersonaRepository.Create(au);
            PersonaResponse res = _Mapper.Map<PersonaResponse>(au);
            return res;
        }

        public List<PersonaResponse> CreateMultiple(List<PersonaRequest> request)
        {
            List<Persona> au = _Mapper.Map<List<Persona>>(request);
            au = _IPersonaRepository.InsertMultiple(au);
            List<PersonaResponse> res = _Mapper.Map<List<PersonaResponse>>(au);
            return res;
        }

        public int Delete(object id)
        {
            return _IPersonaRepository.Delete(id);
        }

        public int deleteMultipleItems(List<PersonaRequest> request)
        {
            List<Persona> au = _Mapper.Map<List<Persona>>(request);
            int cantidad = _IPersonaRepository.DeleteMultipleItems(au);
            return cantidad;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<PersonaResponse> getAll()
        {
            List<Persona> lsl = _IPersonaRepository.GetAll();
            List<PersonaResponse> res = _Mapper.Map<List<PersonaResponse>>(lsl);
            return res;
        }

        public List<PersonaResponse> getAutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public PersonaResponse getById(object id)
        {
            Persona au = _IPersonaRepository.GetById(id);
            PersonaResponse res = _Mapper.Map<PersonaResponse>(au);
            return res;
        }

        public PersonaResponse Update(PersonaRequest entity)
        {
            Persona au = _Mapper.Map<Persona>(entity);
            au = _IPersonaRepository.Update(au);
            PersonaResponse res = _Mapper.Map<PersonaResponse>(au);
            return res;
        }

        public List<PersonaResponse> UpdateMultiple(List<PersonaRequest> request)
        {
            List<Persona> au = _Mapper.Map<List<Persona>>(request);
            au = _IPersonaRepository.UpdateMultiple(au);
            List<PersonaResponse> res = _Mapper.Map<List<PersonaResponse>>(au);
            return res;
        }


        public Persona GetByTipoNroDocumento(string TipoDocumento, string NumeroDocumento)
        {
            Persona vPersona = _IPersonaRepository.GetByTipoNroDocumento(TipoDocumento, NumeroDocumento);

            if (vPersona == null || vPersona.IdPersona == 0)
            {
                if (TipoDocumento.ToLower() == "dni")
                {
                    ApisPeruPersonaResponse pres = _apisPeruServices.PersonaPorDNI(NumeroDocumento);
                    if (pres.success)
                    {
                        vPersona = new Persona();
                        vPersona.NumeroDocumento = pres.dni;
                        vPersona.ApellidoMaterno = pres.apellidoMaterno;
                        vPersona.ApellidoPaterno = pres.apellidoPaterno;
                        vPersona.Nombre = pres.nombres;
                    }
                }
                else if (TipoDocumento.ToLower() == "ruc")
                {
                    ApisPeruEmpresaResponse eres = _apisPeruServices.EmpresaPorRUC(NumeroDocumento);
                    if (!string.IsNullOrEmpty(eres.ruc))
                    {
                        vPersona = new Persona();
                        vPersona.NumeroDocumento = eres.ruc;
                        vPersona.Nombre = eres.razonSocial;
                        // Asignar otros datos de la empresa según sea necesario
                    }
                }
            }
            return vPersona;
        }
        public PersonaResponse GetByIdSub(string sub)
        {
            var persona = _IPersonaRepository.GetByIdSub(sub);
            if (persona != null)
            {
                return _Mapper.Map<PersonaResponse>(persona);
            }
            return null;
        }


        public Persona GetPersonaByDocumento(string documento)
        {
            if (string.IsNullOrEmpty(documento))
            {
                throw new ArgumentException("El documento no puede ser nulo o vacío.");
            }

            Persona persona = _IPersonaRepository.GetByDni(documento);
            if (persona != null)
            {
                // La persona existe, puedes realizar otras operaciones aquí si es necesario
                // Por ejemplo, validar estados, modificar datos adicionales, etc.
                return persona;
            }
            else
            {
                // No se encontró la persona, podrías lanzar una excepción o manejar este caso específico
                return null; // o manejar de otra forma
            }
        }

        public async Task<(List<PersonaResponse>, int)> GetPersonaPaginados(int page, int pageSize)
        {
            var (persona, totalItems) = await _IPersonaRepository.GetPersonaPaginados(page, pageSize);
            var personaResponse = _Mapper.Map<List<PersonaResponse>>(persona);
            return (personaResponse, totalItems);
        }

    }
}
