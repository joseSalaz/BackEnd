using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Service
{
    public class AzureStorage : IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorage(IConfiguration configuration)
        {
            // Obtener la cadena de conexión desde la configuración (Azure)
            string connectionString = configuration["AzureStorage:ConnectionString"];

            // Validar si la cadena de conexión es nula o vacía
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("AzureStorage:ConnectionString", "La cadena de conexión de Azure Storage no está configurada correctamente.");
            }

            // Inicializar el cliente del servicio Blob
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("El archivo es nulo o está vacío.");
            }

            // Crear un cliente para el contenedor Blob
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // Crear el contenedor si no existe
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // Generar un nombre único para el archivo
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Crear un cliente para el archivo Blob
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            // Subir el archivo al contenedor
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            // Devolver la URL del archivo subido
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
