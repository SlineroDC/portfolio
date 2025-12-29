using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Models;
using Portfolio.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Si no existe (en tu PC), usa "portfolio.db".
var dbPath = Environment.GetEnvironmentVariable("DB_PATH") ?? "portfolio.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite($"Data Source={dbPath}"));

// Permitimos localhost Y también la URL de Vercel (que configuraremos en variables)
var allowedOrigins = builder.Configuration["ALLOWED_ORIGINS"] ?? "http://localhost:5173";

builder.Services.AddCors(opt =>
    opt.AddPolicy(
        "AllowVue",
        policy =>
            policy
                .WithOrigins(allowedOrigins.Split(',')) // Convierte "url1,url2" en lista
                .AllowAnyMethod()
                .AllowAnyHeader()
    )
);

// Inyección de dependencias
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<StackService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 2. PIPELINE ---

// Migración automática al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Asegura que la carpeta del volumen exista antes de crear la DB
    // Esto es vital en Linux/Docker si la ruta es /app/data/portfolio.db
    var dbDirectory = Path.GetDirectoryName(dbPath);
    if (!string.IsNullOrEmpty(dbDirectory) && !Directory.Exists(dbDirectory))
    {
        Directory.CreateDirectory(dbDirectory);
    }

    db.Database.EnsureCreated();
}

app.UseCors("AllowVue");

// Para portafolio déjalo activado para que puedas probarlo online.
app.UseSwagger();
app.UseSwaggerUI();

// --- 3. ENDPOINTS ---

// === GRUPO 1: LIVE STATUS ===
app.MapGet(
    "/api/status",
    async (StatusService service) => Results.Ok(await service.GetCurrentStatusAsync())
);

app.MapGet("/api/status/options", (StatusService service) => Results.Ok(service.GetOptions()));

app.MapPost(
    "/api/status/activate/{id}",
    async (int id, StatusService service, IConfiguration config, HttpRequest req) =>
    {
        // OJO: En Railway la variable se llamará AdminKeys__StatusUpdate (con doble guion bajo)
        // o simplemente definimos la key directamente
        var serverKey =
            config["AdminKeys:StatusUpdate"] ?? Environment.GetEnvironmentVariable("ADMIN_KEY");
        var reqKey = req.Headers["X-ADMIN-KEY"];
        if (reqKey != serverKey)
            return Results.Unauthorized();

        return await service.SetManualStatusAsync(id) ? Results.Ok("Activado") : Results.NotFound();
    }
);

// === GRUPO 2: CONTACTO (EMAIL) ===
app.MapPost(
    "/api/contact",
    async (ContactForm form, EmailService mailer) =>
    {
        var sent = await mailer.SendEmailAsync(form);
        return sent ? Results.Ok("Mensaje enviado") : Results.StatusCode(500);
    }
);

// === GRUPO 3: TECH STACK ===
app.MapGet("/api/stack", (StackService service) => Results.Ok(service.GetStack()));

app.Run();
