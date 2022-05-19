using Microsoft.EntityFrameworkCore;

namespace CapTwitch.Model.Model;

public class CapTwitchDbContext : DbContext
{
    public DbSet<StreamEvent> StreamEvents { get; set; }
    public DbSet<StreamRequest> StreamRequests { get; set; }
    public DbSet<User> Users { get; set; }

    public CapTwitchDbContext(DbContextOptions<CapTwitchDbContext> ctx) : base(ctx)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(opt =>
        {
            opt.HasIndex(x => x.Pseudo).IsUnique();
        });
    }
}