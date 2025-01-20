
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace Firebase
{
    public class FirebaseAppManager
    {
        private static readonly Lazy<Task<FirebaseApp>> _firebaseAppInstance =
       new(() => InitializeFirebaseAppAsync());

        public static Task<FirebaseApp> GetInstanceAsync()
        {
            return _firebaseAppInstance.Value;
        }

        private static async Task<FirebaseApp> InitializeFirebaseAppAsync()
        {
            string blobUrl = "https://nuevoblob.blob.core.windows.net/cred/ecomercesa-3c1ff-firebase-adminsdk-bb4c1-e2f8c348e7.json";
            string tempCredentialPath = Path.Combine(Path.GetTempPath(), "firebase-credentials.json");

            if (!File.Exists(tempCredentialPath))
            {
                await DownloadCredentialFileAsync(blobUrl, tempCredentialPath);
            }

            return FirebaseApp.DefaultInstance ?? FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(tempCredentialPath)
            });
        }

        private static async Task DownloadCredentialFileAsync(string blobUrl, string localPath)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(blobUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(localPath, content);
        }

    }
}
