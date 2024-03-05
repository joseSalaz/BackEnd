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
    public interface IApisPeruServices : IDisposable
    {
        ApisPeruPersonaResponse PersonaPorDNI(string dni);
        ApisPeruEmpresaResponse EmpresaPorRUC(string dni);
    }
}
