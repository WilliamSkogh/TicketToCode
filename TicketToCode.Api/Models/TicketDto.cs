using TicketToCode.Core.Models;

namespace TicketToCode.Api.Models
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string TicketType { get; set; } = string.Empty; // ← viktig!
        public string UserEmail { get; set; } = string.Empty;
        public List<DaySelection> SelectedDays { get; set; } = new();
    }
}
