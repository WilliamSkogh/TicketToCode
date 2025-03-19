namespace TicketToCode.Core.Models;

public class Ticket
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event? Event { get; set; }
    public string? UserId { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    
}