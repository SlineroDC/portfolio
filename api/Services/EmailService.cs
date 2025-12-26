using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class EmailService(IConfiguration config)
{
    public async Task<bool> SendEmailAsync(ContactForm form)
    {
        try
        {
            // Cargar configuración desde las variables de entorno (Railway)
            var smtpSettings = config.GetSection("Smtp");

            // Crear el objeto del mensaje
            var message = new MimeMessage();

            // Configurar Remitente (Debe coincidir con tu variable Smtp__FromEmail)
            message.From.Add(new MailboxAddress("Portafolio Bot", smtpSettings["FromEmail"]));

            // Configurar Destinatario (Te envías el correo a ti mismo)
            message.To.Add(new MailboxAddress("Sebastian Linero", smtpSettings["FromEmail"]));

            message.Subject = $"Nuevo mensaje de: {form.Name}";

            // Construir el cuerpo del correo en texto plano
            var builder = new BodyBuilder
            {
                TextBody = $"Nombre: {form.Name}\nEmail: {form.Email}\n\nMensaje:\n{form.Message}",
            };
            message.Body = builder.ToMessageBody();

            // Iniciar cliente SMTP
            using var client = new SmtpClient();

            // Configurar un tiempo de espera máximo de 10 segundos
            // Esto evita que la página se quede "Cargando..." infinitamente si el servidor no responde
            client.Timeout = 10000;

            // Conectar al servidor
            // 'Auto' permite que funcione tanto con puerto 587 (StartTLS) como 465 (SSL Implícito)
            await client.ConnectAsync(
                smtpSettings["Host"],
                int.Parse(smtpSettings["Port"]!),
                SecureSocketOptions.Auto
            );

            // Autenticar con usuario y contraseña de aplicación
            await client.AuthenticateAsync(smtpSettings["Username"], smtpSettings["Password"]);

            // Enviar el mensaje
            await client.SendAsync(message);

            // Cerrar conexión limpiamente
            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception ex)
        {
            // Imprimir el error exacto en los logs de Railway para depuración
            Console.WriteLine($"Error enviando correo: {ex.Message}");
            return false;
        }
    }
}
