using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using IService;
using FirebaseAdmin.Auth;

namespace Service
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly FirebaseStorage _firebaseStorage;

         public async Task<string> DownloadCredentialFileAsync(string blobUrl, string localPath)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(blobUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(localPath, content);
                    return localPath; // Devuelve la ruta local donde guardaste el archivo
                }
                else
                {
                    throw new Exception("Failed to download credential file.");
                }
            }
        }
        public FirebaseStorageService(IConfiguration configuration)
        {
            // Definir la URL de las credenciales en la nube (Azure Blob Storage)
            var blobUrl = configuration["Firebase:CredentialPath"];

            // Ruta local temporal para guardar el archivo de credenciales descargado
            var tempCredentialPath = Path.Combine(Path.GetTempPath(), "firebase-credentials.json");

            // Descargar el archivo de credenciales
            var localFilePath = DownloadCredentialFileAsync(blobUrl, tempCredentialPath).Result;

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(localFilePath) // Usa la ruta local
                });
            }

            // Inicializa Firebase Storage
            var bucket = configuration["Firebase:StorageBucket"];
            _firebaseStorage = new FirebaseStorage(bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = async () =>
                {
                    var firebaseToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync("your-uid");
                    return firebaseToken;
                }
            });
        }


       

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using (var stream = file.OpenReadStream())
            {
                // Carga el archivo en Firebase Storage
                var task = _firebaseStorage
                    .Child(folderName) // Carpeta donde se subirá el archivo
                    .Child(fileName) // Nombre del archivo
                    .PutAsync(stream);

                // Obtén la URL del archivo subido
                var url = await task;
                return url;
            }
        }


        // Nueva función específica para subir imágenes de pedidos
        public async Task<string> UploadPedidosImageAsync(IFormFile image)
        {
            // Llamamos al método UploadFileAsync con la carpeta "pedidosimagenes"
            return await UploadFileAsync(image, "pedidosimagenes");
        }
    }
}
