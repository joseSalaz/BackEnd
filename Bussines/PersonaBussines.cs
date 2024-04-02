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


        //public PersonaResponse BuscarporDNI(string dni)
        //{
        //    Persona persona = _persona.buscarporDNI(dni);
        //    PersonaResponse resultado = new PersonaResponse();
        //    if (persona.IdPersona != 0)
        //    {
        //        resultado = _Mapper.Map<PersonaResponse>(persona);
        //        return resultado;
        //    }
        //    //https://dniruc.apisperu.com/api/v1/dni/12345678?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImFsYmVydG9wYXJpb25hcmFtb3M2QGdtYWlsLmNvbSJ9.l5YJzVRBy16cuBnQ40M8usGf3S39ZiVtLGaPDK8WUuo

        //    string urlApi = "https://dniruc.apisperu.com/api/v1/dni/12345678?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImFsYmVydG9wYXJpb25hcmFtb3M2QGdtYWlsLmNvbSJ9.l5YJzVRBy16cuBnQ40M8usGf3S39ZiVtLGaPDK8WUuo";
        //    List<string> lista = new List<string>();
        //    string resultadoApiPeru = _apisPeruServices.Get("URL", "headresBacios", "token");
        //    ApisPeruPersonaResponse _person = new ApisPeruPersonaResponse();
        //    _person = JsonConvert.Deserialize<ApisPeruPersonaResponse>(resultadoApisPeru);
        //    //string resultadoApisPeru = "{\"success\":true,\"dni\":\"73444819\",\"nombres\":\"JOSE ALBERTO\",\"apellidoPaterno\":\"SALAZAR\",\"apellidoMaterno\":\"CHIRINOS\",\"codVerifica\":\"8\"}"


        //    //newttonsfot
        //    //var jsonString= JsonConvert.SerializeObject(obj);
        //    //var jsonString = JsonSerialize.Serialize(obj, options);


        //    return resultado;   
        //}

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
    }
}
