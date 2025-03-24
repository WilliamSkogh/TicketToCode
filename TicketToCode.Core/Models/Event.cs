namespace TicketToCode.Core.Models;
public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Type { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int MaxAttendees { get; set; }
}

public enum EventType
{
    Concert = 0,
    Festival,
    Theatre,
    Other
}