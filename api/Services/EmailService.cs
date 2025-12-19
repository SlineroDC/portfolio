using System.Net;
using System.Net.Mail;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class EmailService(IConfiguration config)
{
    public async Task<bool> SendEmailAsync(ContactForm form)
    {
        try
        {
            var smtpSettings = config.GetSection("Smtp");
            using var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]!))
            {
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings["FromEmail"]!, "Portafolio Bot"),
                Subject = $"Nuevo mensaje de: {form.Name}",
                Body = $"De: {form.Email}\n\nMensaje:\n{form.Message}",
                IsBodyHtml = false
            };

            // Nos lo enviamos a nosotros mismos
            mailMessage.To.Add(smtpSettings["FromEmail"]!);

            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error enviando correo: {ex.Message}");
            return false;
        }
    }
}
