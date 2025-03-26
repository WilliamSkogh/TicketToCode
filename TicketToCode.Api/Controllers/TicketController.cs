using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketToCode.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketToCode.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly TicketToCodeDbContext _context;

        public TicketController(TicketToCodeDbContext context)
        {
            _context = context;
        }
        
        public record TicketRequest(
            string UserId,
            string? UserEmail,
            TicketType TicketType,
            List<DaySelection> SelectedDays
        );

        // GET: api/ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Ticket>> BuyTicket(TicketRequest request)
        {
            var ticket = new Ticket
            {
                UserId = request.UserId,
                UserEmail = request.UserEmail,
                TicketType = request.TicketType,
                SelectedDays = request.SelectedDays,
                BookingDate = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(BuyTicket), new { id = ticket.Id }, ticket);
        }

    [HttpGet("sold-by-day")]
    public async Task<ActionResult<IEnumerable<TicketsSoldDto>>> GetSoldTicketsPerDay()
{
    var tickets = await _context.Tickets.ToListAsync(); // Ladda alla biljetter först

    var soldPerDay = tickets
        .SelectMany(t => t.SelectedDays)
        .GroupBy(day => day)
        .Select(g => new TicketsSoldDto
        {
            Day = g.Key.ToString(),
            SoldTickets = g.Count()
        })
        .ToList();

    return Ok(soldPerDay);
}

public class TicketsSoldDto
{
    public string Day { get; set; } = string.Empty;
    public int SoldTickets { get; set; }
}

    }
}