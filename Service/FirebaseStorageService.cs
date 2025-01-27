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
using Firebase;

namespace Service
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private FirebaseStorage _firebaseStorage;

        public FirebaseStorageService(IConfiguration configuration)
        {
            InitializeFirebaseAsync(configuration).GetAwaiter().GetResult();
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
            if (_firebaseStorage == null)
            {
                throw new InvalidOperationException("Firebase Storage no se ha inicializado correctamente.");
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using (var stream = file.OpenReadStream())
            {
                var task = _firebaseStorage
                    .Child(folderName)
                    .Child(fileName)
                    .PutAsync(stream);

                var url = await task;
                return url;
            }
        }

        public async Task<string> UploadPedidosImageAsync(IFormFile image)
        {
            return await UploadFileAsync(image, "pedidosimagenes");
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            throw new NotImplementedException();
        }
    }

}
