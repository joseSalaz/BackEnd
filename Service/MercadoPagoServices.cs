using IService;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Configuration;
using Models.RequestResponse;
using PayPal.Api;

public class MercadoPagoService : IPaymentService
{
    private readonly IConfiguration _configuration;

    public MercadoPagoService(IConfiguration configuration)
    {
        _configuration = configuration;
        MercadoPagoConfig.AccessToken = _configuration["MercadoPagoConfig:AccessToken"];
    }


    public async Task<string> CreatePaymentAsync(DatalleCarrito detalleCarrito, decimal total)
    {
        // Construye la lista de items a partir del carrito
        var items = detalleCarrito.Items.Select(item => new PreferenceItemRequest
        {
            Id = item.libro.IdLibro.ToString(), // Convertir a string si es necesario
            Title = item.libro.Titulo,
            Quantity = item.Cantidad,
            UnitPrice = item.PrecioVenta,
            Description = item.libro.Descripcion, // Puedes agregar más información si lo necesitas
            CategoryId = item.libro.IdSubcategoria.ToString() // Cambia según tu lógica
        }).ToList();

        var request = new PreferenceRequest
        {
            AutoReturn = "approved",
            BackUrls = new PreferenceBackUrlsRequest
            {
                Success = "https://libreriasaber.store/detalle-venta",
                Failure = "https://libreriasaber.store/detalle-venta",
                Pending = "https://libreriasaber.store/detalle-venta"
            },
            Items = items,
            Payer = new PreferencePayerRequest
            {
                Email = detalleCarrito.Persona.Correo,
                Name = detalleCarrito.Persona.Nombre,
                Surname = detalleCarrito.Persona.ApellidoPaterno,
                Phone = new MercadoPago.Client.Common.PhoneRequest
                {
                    AreaCode = "11", // Cambia según la lógica de tu aplicación
                    Number = detalleCarrito.Persona.Telefono
                },
                Identification = new MercadoPago.Client.Common.IdentificationRequest
                {
                    Type = detalleCarrito.Persona.TipoDocumento,
                    Number = detalleCarrito.Persona.NumeroDocumento
                },
                Address = new MercadoPago.Client.Common.AddressRequest
                {
                    StreetName = "Calle Ejemplo", // Cambia esto a la dirección real si está disponible
                    StreetNumber = "123", // Cambia esto si tienes el número de la calle
                    ZipCode = "1406" // Cambia esto si tienes el código postal
                }
            },
            PaymentMethods = new PreferencePaymentMethodsRequest
            {
                ExcludedPaymentTypes = new List<PreferencePaymentTypeRequest>
    { 
    },
                Installments = 12,
            },

            NotificationUrl = "https://www.your-site.com/webhook", // Cambia esto a tu URL de webhook real
            Expires = true,
            ExpirationDateFrom = DateTime.UtcNow.AddMinutes(-1), // Cambia esto según tu lógica
            ExpirationDateTo = DateTime.UtcNow.AddDays(1) // Cambia esto según tu lógica
        };

        try
        {
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            // Devuelve la URL para redirigir al usuario al checkout de Mercado Pago
            return preference.InitPoint;
        }
        catch (Exception ex)
        {
            // Manejo de errores adecuado, registro de errores o excepciones si es necesario
            throw new Exception("Error al crear la orden en Mercado Pago", ex);
        }
    }

}




