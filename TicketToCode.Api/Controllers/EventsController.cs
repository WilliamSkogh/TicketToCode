using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketToCode.Core.Data;
using TicketToCode.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace TicketToCode.Api.Controllers;

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
    [HttpPost]
    [Authorize(Roles = "Admin")]  // 🛑 Endast Admin kan skapa event
    public async Task<ActionResult<Event>> CreateEvent([FromBody] Event newEvent)
    {
        if (newEvent == null)
            return BadRequest("Invalid event data");

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvents), new { id = newEvent.Id }, newEvent);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]  // 🛑 Endast Admin kan uppdatera event
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
    {
        var existingEvent = await _context.Events.FindAsync(id);
        if (existingEvent == null)
            return NotFound();

        existingEvent.Name = updatedEvent.Name;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.StartTime = updatedEvent.StartTime;
        existingEvent.EndTime = updatedEvent.EndTime;
        existingEvent.MaxAttendees = updatedEvent.MaxAttendees;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]  
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventToDelete = await _context.Events.FindAsync(id);
        if (eventToDelete == null)
            return NotFound();

        _context.Events.Remove(eventToDelete);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
