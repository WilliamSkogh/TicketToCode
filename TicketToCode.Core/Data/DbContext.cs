using Microsoft.EntityFrameworkCore;
using TicketToCode.Core.Models;

namespace TicketToCode.Core.Data;

public class TicketToCodeDbContext : DbContext
{
    public TicketToCodeDbContext(DbContextOptions<TicketToCodeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Här kan du konfigurera entiteter, relationsregler, constraints etc.
    }
}