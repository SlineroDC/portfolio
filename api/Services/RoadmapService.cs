using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Data;
using Portfolio.Api.Models;

namespace Portfolio.Api.Services;

public class RoadmapService(AppDbContext db)
{
    public async Task<List<RoadmapItem>> GetAllAsync()
    {
        return await db.RoadmapItems.OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<RoadmapItem> AddAsync(RoadmapItem item)
    {
        db.RoadmapItems.Add(item);
        await db.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await db.RoadmapItems.FindAsync(id);
        if (item == null)
            return false;

        db.RoadmapItems.Remove(item);
        await db.SaveChangesAsync();
        return true;
    }
}
