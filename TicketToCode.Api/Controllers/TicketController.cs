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

        // GET: api/ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Ticket>> BuyTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BuyTicket), new { id = ticket.Id }, ticket);
        }

    }
}