using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Models.RequestResponse;

namespace IService
{
    public class ApisPeruServices: IApisPeruServices
    {
        ApisPeruEmpresaResponse empresaResult;
        ApisPeruPersonaResponse personaResult;
        public ApisPeruServices()
        {
            empresaResult = new ApisPeruEmpresaResponse();
            personaResult = new ApisPeruPersonaResponse();
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public ApisPeruEmpresaResponse EmpresaPorRUC(string ruc)
        {
            EmpresaPorRUCTask(ruc).GetAwaiter().GetResult();
            return empresaResult;
        }

        public async Task EmpresaPorRUCTask(string ruc)
        {
            string url = $"https://dniruc.apisperu.com/api/v1/ruc/{ruc}?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImFsYmVydG9wYXJpb25hcmFtb3M2QGdtYWlsLmNvbSJ9.l5YJzVRBy16cuBnQ40M8usGf3S39ZiVtLGaPDK8WUuo";

            using (HttpClient client = new HttpClient())
            {
                // Configurar seguridad TLS
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.StatusCode == HttpStatusCode.OK) // 200
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        empresaResult = JsonConvert.DeserializeObject<ApisPeruEmpresaResponse>(jsonResult);
                    }
                    else
                    {
                        // Manejar error
                        Console.WriteLine($"Error al obtener datos de la empresa. Código de estado: {response.StatusCode}");
                    }
                }
            }
        }



        public ApisPeruPersonaResponse PersonaPorDNI(string dni)
        {
            PersonaPorDNITask(dni).GetAwaiter().GetResult();
            return personaResult;
        }

        public async Task PersonaPorDNITask(string dni)
        {
            string url = "https://dniruc.apisperu.com/api/v1/dni/##DNI##?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImFsYmVydG9wYXJpb25hcmFtb3M2QGdtYWlsLmNvbSJ9.l5YJzVRBy16cuBnQ40M8usGf3S39ZiVtLGaPDK8WUuo";
            url = url.Replace("##DNI##", dni);

            using (HttpClient client = new HttpClient())
            {
                //con la seguridad de C#
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate
                (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

                //https | http

                //EN EL CASO DE QUE LA URL O SERVICIO SOLICITE UN TOKEN
                // ==> client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token jwt");

                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)//200
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();

                    #pragma warning disable CS8601 // Possible null reference assignment.
                        personaResult = JsonConvert.DeserializeObject<ApisPeruPersonaResponse>(jsonResult);
                    }
                    else
                    {
                        //VAMOS A MANEJAR UN CONTROL DE ERRORES
                    }
                }
            }
        }
    }
}
