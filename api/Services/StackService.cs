using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class StackService
{
    // Categorías: frontend, backend, database, tools
    public List<TechItem> GetStack() =>
        [
            // Frontend
            new("Vue.js", "vue", "frontend"),
            new("Tailwind", "tailwind", "frontend"),
            new("JavaScript", "javascript", "frontend"),
            new("HTML5", "html", "frontend"),
            // Backend
            new(".NET Core", "dotnet", "backend"),
            new("C#", "csharp", "backend"),
            new("Node.js", "node", "backend"),
            new("Python", "python", "backend"),
            // Database
            new("SQL Server", "sql", "database"),
            new("MySQL", "mysql", "database"),
            new("MongoDB", "mongo", "database"),
            // Tools
            new("Docker", "docker", "tools"),
            new("Git", "git", "tools"),
            new("GitHub", "github", "tools"),
        ];
}
