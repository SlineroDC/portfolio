using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class StatusService(AppDbContext db)
{
    private readonly List<StatusOption> _options = [
        new(1, "Deep Work", "Staffinity Mode", "code", "green", 2.0),
        new(2, "Gym", "Entrenando", "dumbbell", "orange", 1.5),
        new(3, "F1", "Viendo Carrera", "flag", "red", 2.0),
        new(4, "Reunión", "En llamada", "mic", "purple", 0.5)
    ];
    public List<StatusOption> GetOptions() => _options;

    public async Task<bool> SetManualStatusAsync(int id)
    {
        var opt = _options.FirstOrDefault(o => o.Id == id);
        if (opt is null) return false;
        var state = await db.Settings.FirstOrDefaultAsync() ?? new CurrentState();
        state.ActiveStatusId = id; state.ExpirationTime = DateTime.UtcNow.AddHours(opt.DurationHours);
        if (state.Id == 0) db.Settings.Add(state); else db.Settings.Update(state);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<object> GetCurrentStatusAsync()
    {
        var state = await db.Settings.FirstOrDefaultAsync();
        if (state is { ActiveStatusId: not null } && state.ExpirationTime > DateTime.UtcNow)
        {
            var m = _options.FirstOrDefault(o => o.Id == state.ActiveStatusId);
            if (m != null) return new { m.Title, m.Description, m.Color, m.Icon, isManual = true };
        }
        // Fallback automático (Hora Colombia)
        return DateTime.UtcNow.AddHours(-5).Hour switch
        {
            >= 9 and < 18 => new { Title = "Working", Description = "Disponible", Color = "green", Icon = "briefcase", isManual = false },
            >= 23 or < 6 => new { Title = "Sleeping", Description = "Zzz...", Color = "gray", Icon = "moon", isManual = false },
            _ => new { Title = "Free Time", Description = "Learning", Color = "blue", Icon = "user", isManual = false }
        };
    }
}
