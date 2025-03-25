using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketToCode.Core.Data;
using TicketToCode.Core.Models;

namespace TicketToCode.Api.Controllers;

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
        if (userId == null) return Unauthorized();

        ticket.UserId = userId;
        ticket.BookingDate = DateTime.UtcNow;

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return Ok(ticket);
    }

    [HttpGet("my")]
    [Authorize]
    public IActionResult GetMyTickets()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var myTickets = _context.Tickets
            .Where(t => t.UserId == userId)
            .ToList();

        return Ok(myTickets);
    }

    [HttpGet]
    public IActionResult GetAllTickets() => Ok(_context.Tickets.ToList());
}
