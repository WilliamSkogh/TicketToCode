using Microsoft.EntityFrameworkCore;
using TicketToCode.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TicketToCode.Core.Data;

public class TicketToCodeDbContext : DbContext
{
    public TicketToCodeDbContext(DbContextOptions<TicketToCodeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet <Ticket> Tickets { get; set; }
    
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

          
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasDefaultValue("User");

         
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();
            
            modelBuilder.HasPostgresEnum<DaySelection>();

            
            modelBuilder.Entity<Ticket>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Ticket>()
                .Property(t => t.SelectedDays)
                .HasColumnType("text[]")
                .HasConversion(
                    v => v.Select(d => d.ToString()).ToArray(),
                    v => v.Select(e => Enum.Parse<DaySelection>(e)).ToList()
                )
                .Metadata.SetValueComparer(new ValueComparer<List<DaySelection>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                ));

    }
}
