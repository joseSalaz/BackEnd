﻿using Firebase.Storage;
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

        public FirebaseStorageService(IConfiguration configuration)
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(configuration["Firebase:CredentialPath"])
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
    }
}