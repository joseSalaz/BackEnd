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
using Firebase;

namespace Service
{
    public class OrderMesageFirebase : IOrderMesageFirebase
    {
        private readonly Task<FirebaseApp> _firebaseAppTask;

        public OrderMesageFirebase(IConfiguration configuration)
        {
            _firebaseAppTask = FirebaseAppManager.GetInstanceAsync();
        }

        public async Task<string> SendFirebaseNotificationAsync(string deviceToken, string title, string body, Dictionary<string, string> data = null)
        {
            var firebaseApp = await _firebaseAppTask;

            if (FirebaseMessaging.DefaultInstance == null)
            {
                Console.WriteLine("FirebaseMessaging.DefaultInstance is null. Ensure FirebaseApp is initialized.");
                throw new InvalidOperationException("FirebaseMessaging.DefaultInstance is not initialized.");
            }

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

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
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
