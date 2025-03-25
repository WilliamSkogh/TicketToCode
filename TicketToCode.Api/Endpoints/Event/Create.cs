using Microsoft.EntityFrameworkCore;
namespace TicketToCode.Api.Endpoints;
public class CreateEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/events", Handle)
        .WithSummary("Create event");

    // Request and Response types
    // Why do we need these? check this video if you are not sure
    // https://youtu.be/xtpPspNdX58?si=GJBUxMzeR2ZJ5Fg_
    public record Request(
        string Name,
        string Description,
        EventType Type,
        DateTime Start,
        DateTime End,
        int MaxAttendees
        );
    public record Response(int id);

    //Logic
    private static async Task<Ok<Response>> Handle(Request request, TicketToCodeDbContext db)
{
    var ev = new Event
    {
        Name = request.Name,
        Description = request.Description,
        Type = request.Type,
        StartTime = request.Start.ToUniversalTime(),
        EndTime = request.End.ToUniversalTime(),
        MaxAttendees = request.MaxAttendees
    };

    db.Events.Add(ev);
    await db.SaveChangesAsync();

    return TypedResults.Ok(new Response(ev.Id));
}
}

