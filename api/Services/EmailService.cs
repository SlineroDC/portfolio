using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class EmailService(IConfiguration config)
{
    // Usamos HttpClient estático para mejor rendimiento (conexiones reutilizables)
    private static readonly HttpClient _httpClient = new();

    public async Task<bool> SendEmailAsync(ContactForm form)
    {
        try
        {
            // 1. Obtener la API Key y tu correo de destino desde Railway
            var apiKey = config["Resend:ApiKey"];
            var myEmail = config["Resend:ToEmail"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(myEmail))
            {
                Console.WriteLine("Error: Faltan las variables de entorno de Resend.");
                return false;
            }

            // 2. Construir el cuerpo de la petición (JSON)
            // Resend permite enviar desde 'onboarding@resend.dev' si no tienes dominio propio.
            // Esto asegura que el correo SIEMPRE llegue y no caiga en spam.
            var emailData = new
            {
                from = "Portafolio Bot <onboarding@resend.dev>",
                to = new[] { myEmail },
                subject = $"Nuevo mensaje de: {form.Name}",
                html = $@"
                    <h3>Has recibido un nuevo contacto del Portafolio</h3>
                    <p><strong>Nombre:</strong> {form.Name}</p>
                    <p><strong>Email del contactante:</strong> {form.Email}</p>
                    <hr/>
                    <p><strong>Mensaje:</strong></p>
                    <p>{form.Message}</p>
                ",
                reply_to = form.Email, // Esto permite que si le das "Responder" en Gmail, le respondas a la persona.
            };

            // 3. Preparar la petición HTTP
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails");

            // Autenticación con Bearer Token (La forma estándar de las APIs modernas)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Serializar el objeto a JSON
            var jsonContent = JsonSerializer.Serialize(emailData);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // 4. Enviar la petición a Resend
            var response = await _httpClient.SendAsync(request);

            // 5. Verificar si funcionó
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Correo enviado exitosamente vía Resend API.");
                return true;
            }
            else
            {
                // Si falla, leemos el error que nos devuelve Resend para saber qué pasó
                var errorResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(
                    $"Error enviando correo: {response.StatusCode} - {errorResponse}"
                );
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error crítico en EmailService: {ex.Message}");
            return false;
        }
    }
}
