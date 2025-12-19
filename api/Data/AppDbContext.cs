using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Data;

public class CurrentState
{
    [Key]
    public int Id { get; set; }
    public int? ActiveStatusId { get; set; }
    public DateTime? ExpirationTime { get; set; }
}

public class AppDbContext(DbContextOptions<AppDbContext> o) : DbContext(o)
{
    public DbSet<CurrentState> Settings => Set<CurrentState>();
}
