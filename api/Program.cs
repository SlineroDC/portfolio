using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Models;
using Portfolio.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVICIOS ---
var dbPath = Environment.GetEnvironmentVariable("DB_PATH") ?? "portfolio.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite($"Data Source={dbPath}"));

var allowedOrigins = builder.Configuration["ALLOWED_ORIGINS"] ?? "http://localhost:5173";

builder.Services.AddCors(opt =>
    opt.AddPolicy(
        "AllowVue",
        policy => policy.WithOrigins(allowedOrigins.Split(',')).AllowAnyMethod().AllowAnyHeader()
    )
);

// Inyección de dependencias
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<StackService>();
builder.Services.AddScoped<RoadmapService>(); 

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 2. PIPELINE & DB INIT ---

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Crear directorio si no existe (Para Docker/Railway)
    var dbDirectory = Path.GetDirectoryName(dbPath);
    if (!string.IsNullOrEmpty(dbDirectory) && !Directory.Exists(dbDirectory))
    {
        Directory.CreateDirectory(dbDirectory);
    }

    db.Database.EnsureCreated(); // Crea la DB y Tablas si no existen
    // Seed Data para RoadmapItems si está vacío
    if (!db.RoadmapItems.Any())
    {
        db.RoadmapItems.AddRange(
            new RoadmapItem
            {
                Title = "Aprender GraphQL",
                Tag = "Backend",
                Priority = "Medium",
                Status = "backlog",
            },
            new RoadmapItem
            {
                Title = "Integración IA Gemini",
                Tag = "Feature",
                Priority = "High",
                Status = "doing",
            },
            new RoadmapItem
            {
                Title = "Veterinary CLI App",
                Tag = "C#",
                Priority = "Done",
                Status = "done",
            }
        );
        db.SaveChanges();
    }
}

app.UseCors("AllowVue");

// Swagger activo
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
        var serverKey =
            config["AdminKeys:StatusUpdate"] ?? Environment.GetEnvironmentVariable("ADMIN_KEY");
        var reqKey = req.Headers["X-ADMIN-KEY"];
        if (reqKey != serverKey)
            return Results.Unauthorized();
        return await service.SetManualStatusAsync(id) ? Results.Ok("Activado") : Results.NotFound();
    }
);

// === GRUPO 2: CONTACTO ===
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

// === GRUPO 4: ROADMAP (KANBAN) ===
app.MapGet(
    "/api/roadmap",
    async (RoadmapService service) => Results.Ok(await service.GetAllAsync())
);

app.MapPost(
    "/api/roadmap",
    async (RoadmapItem item, RoadmapService service) =>
    {
        var created = await service.AddAsync(item);
        return Results.Created($"/api/roadmap/{created.Id}", created);
    }
);

app.MapDelete(
    "/api/roadmap/{id}",
    async (int id, RoadmapService service) =>
        await service.DeleteAsync(id) ? Results.NoContent() : Results.NotFound()
);

app.Run();
