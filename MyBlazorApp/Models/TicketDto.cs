using TicketToCode.Core.Models;

namespace MyBlazorApp.Models
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string TicketType { get; set; } = string.Empty; // ✅ viktigaste!
        public string UserEmail { get; set; } = string.Empty;
        public List<DaySelection> SelectedDays { get; set; } = new();
    }
}
