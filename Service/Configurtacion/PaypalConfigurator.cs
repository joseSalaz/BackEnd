using Microsoft.Extensions.Configuration;
using PayPal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Configurtacion
{
    public static class PaypalConfigurator
    {
        public static Dictionary<string, string> GetPaypalConfig(IConfiguration configuration)
        {
            return new Dictionary<string, string>
        {
            { "clientId", configuration.GetValue<string>("PayPal:ClientId") },
            { "clientSecret", configuration.GetValue<string>("PayPal:ClientSecret") },
            { "mode", configuration.GetValue<string>("PayPal:Mode") }
        };
        }

        public static APIContext GetAPIContext(IConfiguration configuration)
        {
            var config = GetPaypalConfig(configuration);
            string clientId = config["clientId"];
            string clientSecret = config["clientSecret"];
            string mode = config["mode"];

            var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();
            var apiContext = new APIContext(accessToken) { Config = config };

            return apiContext;
        }
    }
}
