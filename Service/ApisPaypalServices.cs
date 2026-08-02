using IService;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using PayPal.Api;

namespace Service;



public class ApisPaypalServices : IApisPaypalServices
{
    private readonly IConfiguration _config;
    public ApisPaypalServices(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Payment> CreateOrdersasync(DatalleCarrito deCarrito, decimal amount, string returnUrl, string cancelUrl)
    {
        var clientId = _config["PayPalSettings:ClientId"];
        var clientSecret = _config["PayPalSettings:Secret"];
        var mode = _config["PayPalSettings:Mode"];

        // Crear OAuthTokenCredential con las credenciales y el modo obtenidos de la configuración
        var tokenCredential = new OAuthTokenCredential(clientId, clientSecret, new Dictionary<string, string> { { "mode", mode } });
        var accessToken = tokenCredential.GetAccessToken();
        var apiContext = new APIContext(accessToken);
        apiContext.Config = new Dictionary<string, string>(){
            { "mode", mode }
        };

        // Convertir los ítems del carrito a ítems de PayPal
        List<Item> paypalItems = deCarrito.Items.Select(itemCarrito => new Item
        {
            name = itemCarrito.libro.Titulo,
            currency = "USD", // Asegúrate de que la moneda es compatible con tu cuenta de PayPal
            price = itemCarrito.PrecioVenta.ToString("F2"), // Formato de precio a dos decimales
            quantity = itemCarrito.Cantidad.ToString(),
            sku = itemCarrito.libro.IdLibro.ToString() // SKU es opcional, pero útil para el seguimiento
        }).ToList();

        // Crear la lista de transacciones
        var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    description = "Transacción de la tienda online",
                    invoice_number = new Random().Next(999999).ToString(), // Número de factura único
                    amount = new Amount
                    {
                        currency = "USD",
                        total = amount.ToString("F2") // Usa el monto total pasado al método
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

        try
        {
            // Crear el pago con el contexto de la API de forma asincrónica

            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }
        catch (Exception ex)
        {
            // Loguear la excepción y manejarla adecuadamente
            // No expongas información sensible de la excepción al usuario
            // throw; // Re-lanzar la excepción o manejarla según la política de errores de tu aplicación.
            Console.WriteLine(ex.Message);
            return null; // O manejar de otra forma que prefieras
        }
    }


}



