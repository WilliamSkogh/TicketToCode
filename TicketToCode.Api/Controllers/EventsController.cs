using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly TicketToCodeDbContext _context;//ändrat från ApplicationDbContext

    public EventsController(TicketToCodeDbContext context) //ändrat från ApplicationDbContext
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        return await _context.Events.ToListAsync();
    }
}
