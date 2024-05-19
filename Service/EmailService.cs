using IService;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;

namespace Service
{
    public class EmailService : IEmailService
    {
        //    // Configuración de SMTP
        //    private string host = "smtp.gmail.com";
        //    private int port = 587; // Asegúrate de usar el puerto correcto para tu caso
        //    private bool enableSSL = true;
        //    private string smtpUsername = "libreriasabers@gmail.com"; // Usa tu correo real aquí
        //    private string smtpPassword = "u h z f c b i n e c h h u z e c"; // Usa tu contraseña real aquí

        //    public async Task SendEmailAsync(string to, string subject, string body, Stream attachment, string attachmentName)
        //    {
        //        using (SmtpClient smtpClient = new SmtpClient(host, port))
        //        {
        //            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
        //            smtpClient.EnableSsl = enableSSL;

        //            // Configuraciones adicionales para debugging
        //            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtpClient.UseDefaultCredentials = false;
        //            smtpClient.DeliveryFormat = SmtpDeliveryFormat.International;

        //            using (MailMessage message = new MailMessage())
        //            {
        //                message.From = new MailAddress(smtpUsername, "Libreria Saber");
        //                message.To.Add(new MailAddress(to));
        //                message.Subject = subject;
        //                message.Body = body;

        //                if (attachment != null)
        //                {
        //                    attachment.Position = 0; // Asegúrate de que el puntero esté al inicio del stream
        //                    Attachment pdfAttachment = new Attachment(attachment, attachmentName, "application/pdf");
        //                    message.Attachments.Add(pdfAttachment);
        //                }

        //                try
        //                {
        //                    await smtpClient.SendMailAsync(message);
        //                    Console.WriteLine("Correo enviado exitosamente.");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"Error al enviar el correo: {ex.Message}");
        //                    throw;
        //                }
        //            }
        //        }
        //    }
        //}



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
                    message.Subject = subject;
                    message.Body = body;

                    if (attachment != null)
                    {
                        attachment.Position = 0;
                        Attachment pdfAttachment = new Attachment(attachment, attachmentName, "application/pdf");
                        message.Attachments.Add(pdfAttachment);
                    }

                    try
                    {
                        await smtpClient.SendMailAsync(message);
                        Console.WriteLine("Correo enviado exitosamente al cliente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al enviar el correo al cliente: {ex.Message}");
                        throw;
                    }
                }

                // Enviar correo de confirmación al remitente
                using (MailMessage confirmationMessage = new MailMessage())
                {
                    confirmationMessage.From = new MailAddress(smtpUsername, "Libreria Saber");
                    confirmationMessage.To.Add(new MailAddress(smtpUsername)); // Enviar confirmación al mismo correo del remitente
                    confirmationMessage.Subject = "Confirmación de venta realizada";
                    confirmationMessage.Body = $"Se ha realizado una venta y se ha enviado un correo a {to}.";

                    try
                    {
                        await smtpClient.SendMailAsync(confirmationMessage);
                        Console.WriteLine("Correo de confirmación enviado exitosamente al remitente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("no se puedo hacer el envio");
                        throw;
                    }
                }
            }
        }


      

    }
}
