using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using FirebaseAdmin.Auth;

public class FirebaseStorageService : IFirebaseStorageService
{
    private  FirebaseStorage _firebaseStorage;
    private readonly string _credentialPath;
    private readonly IConfiguration _configuration;

    public FirebaseStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _credentialPath = Path.Combine(Path.GetTempPath(), "firebase-credentials.json");

        // Inicializar Firebase de forma asíncrona
        _ = InitializeFirebaseAsync();
    }

    private async Task InitializeFirebaseAsync()
    {
        var blobUrl = _configuration["Firebase:CredentialPath"];

        try
        {
            var localFilePath = await DownloadCredentialFileAsync(blobUrl, _credentialPath);

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(localFilePath)
                });
            }

            var bucket = _configuration["Firebase:StorageBucket"];
            _firebaseStorage = new FirebaseStorage(bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = async () =>
                {
                    var firebaseToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync("your-uid");
                    return firebaseToken;
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al inicializar Firebase: {ex.Message}");
        }
    }

    public async Task<string> DownloadCredentialFileAsync(string blobUrl, string localPath)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(blobUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(localPath, content);
                return localPath;
            }
            else
            {
                throw new Exception("Failed to download credential file.");
            }
        }
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folderName)
    {
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
}
