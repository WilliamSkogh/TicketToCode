namespace TicketToCode.Core.Models;

public class TicketsSold
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int SoldTickets { get; set; } = 0;
    public int MaxTickets { get; set; } = 200;
}