using Azure.Storage.Blobs;
using IService;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class AzureStorage : IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorage(IConfiguration configuration)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString")
                                      ?? configuration["AzureStorage:ConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("AzureStorage:ConnectionString", "La cadena de conexión de Azure Storage no está configurada correctamente.");
            }

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
