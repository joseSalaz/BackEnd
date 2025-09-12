using DBModel.DB;
using IService;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;

namespace Service
{
    public class EmailService : IEmailService
    {

        // Configuración de SMTP
        private string host = "smtp.gmail.com";
        private int port = 587;
        private bool enableSSL = true;
        private string smtpUsername = "libreriasabers@gmail.com"; // Correo real
        private string smtpPassword = "u h z f c b i n e c h h u z e c"; // Contraseña real

        public async Task SendEmailAsync(string to, string subject, string body, Stream attachment, string attachmentName)
        {
            using (SmtpClient smtpClient = new SmtpClient(host, port))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = enableSSL;
                // Configuraciones adicionales para debugging
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;

                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(smtpUsername, "Libreria Saber");
                    message.To.Add(new MailAddress(to));
                    message.Subject = "Comprobante de Compra - " + subject;
                    message.IsBodyHtml = true;
                    message.Body = $@"
                <html>
                <body style='font-family: Arial, sans-serif;'>
                    <p>Estimado cliente,</p>
                    <p>Adjunto encontrará su comprobante de compra realizada en Libreria Saber.</p>
                    <p>Gracias por su preferencia.</p>
                    <br/>
                    <p>Saludos cordiales,</p>
                    <p><strong>Libreria Saber</strong></p>
                    <p style='color: #666; font-size: 12px;'>Este es un correo automático, por favor no responder.</p>
                </body>
                </html>";

                    if (attachment != null)
                    {
                        attachment.Position = 0;
                        Attachment pdfAttachment = new Attachment(attachment, attachmentName, "application/pdf");
                        message.Attachments.Add(pdfAttachment);
                    }

                    try
                    {
                        await smtpClient.SendMailAsync(message);
                        Console.WriteLine("Comprobante enviado exitosamente al cliente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al enviar el comprobante al cliente: {ex.Message}");
                        throw;
                    }
                }

                // Enviar correo de confirmación al remitente
                using (MailMessage confirmationMessage = new MailMessage())
                {
                    confirmationMessage.From = new MailAddress(smtpUsername, "Libreria Saber");
                    confirmationMessage.To.Add(new MailAddress(smtpUsername));
                    confirmationMessage.Subject = "Registro de comprobante enviado";
                    confirmationMessage.Body = $"Se ha enviado un comprobante de compra al cliente: {to}";

                    try
                    {
                        await smtpClient.SendMailAsync(confirmationMessage);
                        Console.WriteLine("Registro interno guardado exitosamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al guardar el registro interno.");
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Enviar notificación al cliente cuando su pedido cambia de estado.
        /// </summary>
        public async Task SendOrderStatusUpdateEmailAsync(string to, int orderId, string newStatus, List<DetalleVenta> productos, List<string> imagenesProductos)
        {
            string subject = $"Actualización de estado de tu pedido #{orderId}";
            string orderUrl = $"https://Libreriasaber.store/pedido/{orderId}";

            // Construir dinámicamente la lista de productos en HTML
            string productosHtml = "";
            for (int i = 0; i < productos.Count; i++)
            {
                var producto = productos[i];
                var imagenUrl = i < imagenesProductos.Count ? imagenesProductos[i] : "";

                productosHtml += $@"
<div class=""product"">
    <div class=""product-info"">
        <div class=""product-image"">
            <img src=""{imagenUrl}"" alt=""{producto.NombreProducto}"" class=""product-img"">
        </div>
        <div class=""product-details-container"">
            <p class=""product-name"">{producto.NombreProducto}</p>
            <p class=""product-details"">Cantidad: {producto.Cantidad}</p>
        </div>
        <div class=""product-price"">
            <p>S/{producto.PrecioUnit:F2}</p>
            <p>Total: S/{producto.Importe:F2}</p>
        </div>
    </div>
</div>";
            }

            decimal totalImporte = productos.Sum(p => p.Importe ?? 0m);

            string body = $@"
<html>
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <title>Detalle de tu Pedido</title>
    <style>
        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 20px;
            color: #2d3748;
            line-height: 1.6;
        }}
        .container {{
            max-width: 600px;
            background: #ffffff;
            margin: 0 auto;
            padding: 32px;
            border-radius: 16px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
        }}
        .header {{
            text-align: center;
            margin-bottom: 32px;
            padding-bottom: 24px;
            border-bottom: 2px solid #f1f1f1;
        }}
        .logo {{
            width: 120px;
            height: auto;
            margin-bottom: 16px;
        }}
        h2 {{
            color: #1a202c;
            font-size: 24px;
            margin: 0 0 8px 0;
            font-weight: 600;
        }}
        .status-badge {{
            display: inline-block;
            padding: 8px 16px;
            background-color: #ebf8ff;
            color: #2b6cb0;
            border-radius: 8px;
            font-weight: 500;
            margin: 16px 0;
        }}
        .product {{
            background: #f8fafc;
            border-radius: 12px;
            padding: 20px;
            margin-bottom: 16px;
        }}
        .product-info {{
            display: grid;
            grid-template-columns: auto 2fr 1fr;
            gap: 16px;
            align-items: center;
        }}
        .product-image {{
            width: 80px;
            height: 80px;
            overflow: hidden;
            border-radius: 8px;
        }}
        .product-img {{
            width: 100%;
            height: 100%;
            object-fit: cover;
        }}
        .product-details-container {{
            flex: 1;
        }}
        .product-name {{
            font-weight: 600;
            font-size: 16px;
            color: #2d3748;
            margin: 0 0 8px 0;
        }}
        .product-details {{
            font-size: 14px;
            color: #4a5568;
            margin: 4px 0;
        }}
        .product-price {{
            text-align: right;
            font-weight: 600;
            color: #2d3748;
        }}
        .footer {{
            text-align: center;
            margin-top: 32px;
            padding-top: 24px;
            border-top: 2px solid #f1f1f1;
        }}
        .button-container {{
            display: flex;
            gap: 16px;
            justify-content: center;
            margin-top: 24px;
        }}
        .button {{
            display: inline-block;
            padding: 12px 24px;
            font-weight: 500;
            text-decoration: none;
            border-radius: 8px;
            transition: all 0.3s ease;
        }}
        .button-primary {{
            background-color: #3182ce;
            color: white;
        }}
        .button-primary:hover {{
            background-color: #2c5282;
        }}
        .button-secondary {{
            background-color: #1a202c;
            color: white;
        }}
        .button-secondary:hover {{
            background-color: #2d3748;
        }}
        .total-section {{
            margin-top: 24px;
            padding: 16px;
            background: #f8fafc;
            border-radius: 12px;
            text-align: right;
        }}
        .total-amount {{
            font-size: 18px;
            font-weight: 600;
            color: #1a202c;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <img src=""https://firebasestorage.googleapis.com/v0/b/ecomercesa-3c1ff.appspot.com/o/ic_launcher.png?alt=media&token=65889eaa-978d-4264-8ee2-e005e568a1e5"" alt=""Librería Saber"" class=""logo"">
            <h2>Pedido #{orderId} actualizado</h2>
            <div class=""status-badge"">{newStatus}</div>
        </div>

        <div class=""products"">
            {productosHtml}
        </div>

        <div class=""total-section"">
            <span class=""total-amount"">Total: S/{totalImporte:F2}</span>
        </div>

        <div class=""footer"">
            <p>¿Tienes alguna pregunta? Contáctanos a través de nuestros canales de atención.</p>
            <div class=""button-container"">
                <a href=""{orderUrl}"" class=""button button-primary"">Rastrear pedido</a>
                <a href=""{orderUrl}"" class=""button button-secondary"">Ver detalles</a>
            </div>
        </div>
    </div>
</body>
</html>";

            using (SmtpClient smtpClient = new SmtpClient(host, port))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = enableSSL;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;

                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(smtpUsername, "Librería Saber");
                    message.To.Add(new MailAddress(to));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    try
                    {
                        await smtpClient.SendMailAsync(message);
                        Console.WriteLine($"Correo de actualización enviado a {to} para el pedido #{orderId}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al enviar la actualización de estado: {ex.Message}");
                        throw;
                    }
                }
            }
        }

    }
}
