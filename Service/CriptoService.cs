using IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilSecurity.UtilSecurity;

namespace Service
{
    public class CriptoService : ICriptoService
    {
        public string Desencriptar(string encryptedText)
        {
            return UtilCripto.desencriptar_AES(encryptedText);
        }
    }
}
