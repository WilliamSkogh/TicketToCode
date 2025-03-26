namespace TicketToCode.Core.Models;

public class Ticket
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public TicketType TicketType { get; set; }
    public string? UserEmail { get; set; }
    public List<DaySelection>? SelectedDays { get; set; } = new();
    
}

public enum TicketType
{
    OneDay = 1,
    TwoDays = 2,
    ThreeDays = 3,
}

public enum DaySelection
{
    Friday = 1,
    Saturday = 2,
    Sunday = 3
}
