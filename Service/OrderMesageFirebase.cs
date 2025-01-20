using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using IService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service
{
    public class OrderMesageFirebase : IOrderMesageFirebase
    {
        private readonly FirebaseApp _firebaseApp;

        public OrderMesageFirebase(IConfiguration configuration)
        {
            var blobUrl = configuration["FirebaseMessaging:CredentialPath"];
            var tempCredentialPath = Path.Combine(Path.GetTempPath(), "firebase-messaging-credentials.json");

            try
            {
                var localFilePath = DownloadCredentialFileAsync(blobUrl, tempCredentialPath).Result;

                if (FirebaseApp.DefaultInstance == null) // Verifica si hay una instancia por defecto
                {
                    _firebaseApp = FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromFile(localFilePath)
                    });
                    Console.WriteLine("Firebase Messaging App initialized from file.");
                }
                else
                {
                    _firebaseApp = FirebaseApp.DefaultInstance; // Obtén la instancia por defecto
                    Console.WriteLine("Firebase Messaging App instance retrieved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Firebase Messaging: {ex.Message}");
                throw; // Re-lanza la excepción
            }
        }

        private async Task<string> DownloadCredentialFileAsync(string blobUrl, string localPath)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(blobUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(localPath, content);
                    return localPath;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error downloading credential file: {ex.Message}. Status Code: {ex.StatusCode}");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading credential file: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<string> SendFirebaseNotificationAsync(string deviceToken, string title, string body, Dictionary<string, string> data = null)
        {
            try
            {
                var message = new Message()
                {
                    Token = deviceToken,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                    },
                    Data = data
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message); // No pasa la instancia _firebaseApp aquí
                Console.WriteLine($"Successfully sent message: {response}");
                return response;
            }
            catch (FirebaseMessagingException e)
            {
                Console.WriteLine($"Error sending message: {e.Message}");
                return null;
            }
        }
    }
}
