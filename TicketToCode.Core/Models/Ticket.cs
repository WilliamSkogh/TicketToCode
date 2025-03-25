namespace TicketToCode.Core.Models;

public class Ticket
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty; 
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;

    public int EventId { get; set; } 
    public Event? Event { get; set; } 

    public TicketType TicketType { get; set; }

    public List<DateTime> SelectedDays { get; set; } = new(); 

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public int Quantity { get; set; } = 1;
    public PaymentMethod PaymentMethod { get; set; }
}

public enum TicketType
{
    OneDay = 1,
    TwoDays = 2,
    ThreeDays = 3,
}

public enum PaymentMethod
{
    Card = 0,
    Swish,
    Invoice
}
