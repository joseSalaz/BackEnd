using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using PayPal.Api;
namespace Service
{
    public class ApisPaypalServices : IApisPaypalServices
    {
        private readonly IConfiguration _config;
        public ApisPaypalServices(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Payment> CreateOrdersasync(DatalleCarrito deCarrito,decimal amount, string returnUrl, string cancelUrl)
        {
            var clientId = _config["PayPalSettings:ClientId"];
            var clientSecret = _config["PayPalSettings:ClientSecret"];
            var mode = _config["PayPalSettings:Mode"];
            var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();
            var apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret, new Dictionary<string, string>() { { "mode", mode } }).GetAccessToken())
            {
                Config = new Dictionary<string, string>() { { "mode", mode } }
            };
            List<Item> paypalItems = deCarrito.Items.Select(itemCarrito => new Item
            {
                name = itemCarrito.libro.Titulo,
                currency = "PEN",
                price = itemCarrito.PrecioVenta.ToString("F2"),
                quantity = itemCarrito.Cantidad.ToString(),
                sku = itemCarrito.libro.IdLibro.ToString() 
            }).ToList();

            var transactionList = new List<Transaction>()
    {
        new Transaction()
        {
            description = "Transacción de la tienda online",
            invoice_number = new Random().Next(999999).ToString(),
            amount = new Amount()
            {
                currency = "PEN",
                total = deCarrito.Items.Sum(i => i.PrecioVenta * i.Cantidad).ToString("F2") 
            },
            item_list = new ItemList
            {
                items = paypalItems
            }
        }
    };

            var payer = new Payer { payment_method = "paypal" };
            var redirectUrls = new RedirectUrls
            {
                cancel_url = cancelUrl,
                return_url = returnUrl
            };

            var payment = new Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };

            var createdPayment = await Task.Run(() => payment.Create(apiContext));
            return createdPayment;
        }

    }

}

