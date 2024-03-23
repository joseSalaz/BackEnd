using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AzureStorage : IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorage(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("AzureStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            // Crear un cliente de contenedor Blob
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("imagenes");
            await blobContainerClient.CreateIfNotExistsAsync();

            // Generar un nombre único para el archivo
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Subir archivo al contenedor
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            // Devolver la URL de la imagen
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
