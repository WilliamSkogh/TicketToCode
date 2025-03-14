namespace TicketToCode.Api.Endpoints;
public class GetAllEvents : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events", Handle)
        .WithSummary("Get all events");

    // Request and Response types
    public record Response(
        int Id,
        string Name,
        string Description,
        EventType Type,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    //Logic
    private static IResult Handle(TicketToCodeDbContext db)
    {
        var events = db.Events
            .Select(item => new Response(
                item.Id,
                item.Name,
                item.Description,
                item.Type,
                item.StartTime,
                item.EndTime,
                item.MaxAttendees
            )).ToList();
        return Results.Ok(events);
    }
}