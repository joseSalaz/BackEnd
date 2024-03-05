using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace UtilSecurity
{
    namespace UtilSecurity
    {
        public class UtilCripto
        {

            public static string encriptar_AES(string clearText)
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(ObtnerSecretKey(), new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }


            private static string ObtnerSecretKey()
            {
                IConfigurationBuilder configurationBuild = new ConfigurationBuilder();
                configurationBuild = configurationBuild.AddJsonFile("appsettings.json");
                IConfiguration configurationFile = configurationBuild.Build();
                // Leemos el archivo de configuración.
                string str = configurationFile.GetSection("SecretKey").Value;
                return str;

            }

        }
    }
}