using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketToCode.Core.Data;
using TicketToCode.Core.Models;

namespace TicketToCode.Api.Endpoints;

[ApiController]
[Route("tickets")]
public class TicketsController : ControllerBase
{
    private readonly TicketToCodeDbContext _context;

    public TicketsController(TicketToCodeDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize] 
    public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;


        if (userId != null)
        {
            ticket.UserId = userId;
            ticket.UserEmail = userEmail;
        }

        ticket.BookingDate = DateTime.UtcNow;

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return Ok(ticket);
    }


    [HttpGet]
    public IActionResult GetAllTickets()
    {
        return Ok(_context.Tickets.ToList());
    }
}
