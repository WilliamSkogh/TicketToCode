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
  
    }
}
