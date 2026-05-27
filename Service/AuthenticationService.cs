using Google.Apis.Auth;
using IRepository;
using IService;
using Models.RequestRequest;
using Models.RequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPersonaRepository _personaRepository;

        public AuthenticationService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        //public async Task<PersonaResponse> AuthenticateWithGoogleAsync(string googleToken)
        //{
        //    GoogleJsonWebSignature.Payload payload = null;
        //    try
        //    {
        //        payload = await VerifyGoogleToken(googleToken);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo adecuado del error
        //        throw new ArgumentException("Token inválido o expirado.", ex);
        //    }

        //    if (payload == null)
        //    {
        //        throw new ArgumentException("El payload del token de Google es nulo.");
        //    }

        //    // Aquí asumimos que tienes una función en tu repositorio para buscar a la persona por su correo electrónico
        //    var persona = await _personaRepository.GetByCorreoAsync(payload.Email);

        //    if (persona == null)
        //    {
        //        // Si la persona no existe, crea una nueva entrada en la base de datos
        //        persona = new PersonaResponse
        //        {
        //            Nombre = payload.GivenName,
        //            ApellidoPaterno = payload.FamilyName, // Asumiendo que quieres separar el apellido
        //            Correo = payload.Email,
        //            // Otros campos que desees asignar
        //        };

        //        // Asumiendo que tienes un método en tu repositorio para crear una nueva persona
        //        persona = await _personaRepository.Create(persona);
        //    }

        //    // Convierte la entidad Persona a un PersonaResponse
        //    // Asumiendo que tienes un método para esto, como un mapeo o algo similar
        //    var personaResponse = MapToPersonaResponse(persona);

        //    return personaResponse;
        //}


        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string googleToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { "tu-cliente-id.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken, settings);
            return payload;
        }
    }
}
