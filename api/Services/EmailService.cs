using MailKit.Net.Smtp;
using MimeKit;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class EmailService(IConfiguration config)
{
    public async Task<bool> SendEmailAsync(ContactForm form)
    {
        try
        {
            // 1. Obtener la configuración desde las variables de entorno (Railway)
            var smtpSettings = config.GetSection("Smtp");

            // 2. Crear el objeto del mensaje (MimeMessage)
            var message = new MimeMessage();

            // Configurar el remitente (Debe coincidir con la variable Smtp__FromEmail)
            message.From.Add(new MailboxAddress("Portafolio Bot", smtpSettings["FromEmail"]));

            // Configurar el destinatario (Te lo envías a ti mismo)
            message.To.Add(new MailboxAddress("Sebastian Linero", smtpSettings["FromEmail"]));

            // Configurar el asunto
            message.Subject = $"Nuevo mensaje de: {form.Name}";

            // 3. Construir el cuerpo del correo
            var builder = new BodyBuilder
            {
                // Usamos TextBody para un formato simple y limpio
                TextBody = $"Nombre: {form.Name}\nEmail: {form.Email}\n\nMensaje:\n{form.Message}"
            };
            message.Body = builder.ToMessageBody();

            // 4. Iniciar el cliente SMTP (MailKit)
            using var client = new SmtpClient();

            // Conectar al servidor (Gmail usa puerto 587 con cifrado StartTls)
            await client.ConnectAsync(
                smtpSettings["Host"],
                int.Parse(smtpSettings["Port"]!),
                MailKit.Security.SecureSocketOptions.StartTls
            );

            // Autenticarse con las credenciales (Correo y Contraseña de Aplicación)
            await client.AuthenticateAsync(
                smtpSettings["Username"],
                smtpSettings["Password"]
            );

            // Enviar el mensaje
            await client.SendAsync(message);

            // Desconectar limpiamente cerrando la sesión
            await client.DisconnectAsync(true);

            return true; // Envío exitoso
        }
        catch (Exception ex)
        {
            // Registrar el error en la consola para depuración en los logs
            Console.WriteLine($"Error enviando correo: {ex.Message}");
            return false; // Envío fallido
        }
    }
}