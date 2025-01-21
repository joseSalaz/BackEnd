using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Net.Http;
using System.Text.Json;

namespace Firebase
{
    public class FirebaseAppManager
    {
        private static readonly Lazy<Task<FirebaseApp>> _firebaseAppInstance =
            new(() => InitializeFirebaseAppAsync());

        public static async Task<FirebaseApp> GetInstanceAsync()
        {
            try
            {
                return await _firebaseAppInstance.Value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("FirebaseApp failed to initialize.", ex);
            }
        }

        private static async Task<FirebaseApp> InitializeFirebaseAppAsync()
        {
            string blobUrl = "https://nuevoblob.blob.core.windows.net/cred/ecomercesa-3c1ff-firebase-adminsdk-bb4c1-e2f8c348e7.json";
            string tempCredentialPath = Path.Combine(Path.GetTempPath(), "firebase-credentials.json");

            try
            {
                // Download and validate credentials
                string jsonContent = await DownloadAndValidateCredentialsAsync(blobUrl);

                // Write valid JSON to temp file
                await File.WriteAllTextAsync(tempCredentialPath, jsonContent);

                // Create Firebase app
                return FirebaseApp.DefaultInstance ?? FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(tempCredentialPath)
                });
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Failed to download credentials from blob storage: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Invalid JSON credential format: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize Firebase: {ex.Message}", ex);
            }
        }

        private static async Task<string> DownloadAndValidateCredentialsAsync(string blobUrl)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(blobUrl);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            // Log first few characters for debugging (be careful not to log entire credential file)
            Console.WriteLine($"First 20 characters of downloaded content: {content.Substring(0, Math.Min(20, content.Length))}");

            // Validate JSON structure
            try
            {
                using var document = JsonDocument.Parse(content);

                // Verify essential Firebase credential properties
                var root = document.RootElement;

                if (!root.TryGetProperty("type", out var type) ||
                    !root.TryGetProperty("project_id", out var projectId) ||
                    !root.TryGetProperty("private_key", out var privateKey))
                {
                    throw new JsonException("Missing required Firebase credential properties");
                }

                return content;
            }
            catch (JsonException)
            {
                // Log the content length and first few characters for debugging
                Console.WriteLine($"Downloaded content length: {content.Length}");
                Console.WriteLine($"Content starts with: {content.Substring(0, Math.Min(50, content.Length))}");
                throw;
            }
        }
    }
}