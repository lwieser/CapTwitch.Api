using Microsoft.EntityFrameworkCore;

namespace CapTwitch.Api.Model;

public class CapTwitchDbContext : DbContext
{
    public DbSet<StreamEvent> StreamEvents { get; set; }
    public DbSet<StreamRequest> StreamRequests { get; set; }

    public CapTwitchDbContext(DbContextOptions<CapTwitchDbContext> ctx) : base(ctx)
    {
            
    }
}