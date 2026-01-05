namespace Portfolio.Api.Models;

public class RoadmapItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Tag { get; set; }
    public required string Priority { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
