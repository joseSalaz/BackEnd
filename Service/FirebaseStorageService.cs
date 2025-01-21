using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using FirebaseAdmin.Auth;
using Firebase;

namespace Service
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private FirebaseStorage _firebaseStorage;

        public FirebaseStorageService(IConfiguration configuration)
        {
            _ = InitializeFirebaseAsync(configuration);
        }

        private async Task InitializeFirebaseAsync(IConfiguration configuration)
        {
            var firebaseApp = await FirebaseAppManager.GetInstanceAsync();
            var bucket = configuration["Firebase:StorageBucket"];

            _firebaseStorage = new FirebaseStorage(bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = async () =>
                {
                    var firebaseToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync("generic-uid");
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

                var url = await task;
                return url;
            }
        }

        public async Task<string> UploadPedidosImageAsync(IFormFile image)
        {
            return await UploadFileAsync(image, "pedidosimagenes");
        }
    }

}
