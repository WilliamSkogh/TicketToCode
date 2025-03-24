using System.ComponentModel.DataAnnotations;

namespace TicketToCode.Shared.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string? UserEmail { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
}

public enum PaymentMethod
{
    Card = 0,
    Swish,
    Invoice
}
