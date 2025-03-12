namespace TicketToCode.Api.Endpoints;
public class GetEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events/{id}", Handle)
        .WithSummary("Get event");

    // Request and Response types
    public record Request(int Id);

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
    private static IResult Handle([AsParameters] Request request, IDatabase db)
    {
        var ev = db.Events.Find(ev => ev.Id == request.Id);

        if (ev == null)
        {
            return Results.NotFound($"Event with id {request.Id} not found");
        }

        // map ev to response dto
        var response = new Response(
            Id: ev.Id,
            Name: ev.Name,
            Description: ev.Description,
            Type: ev.Type,
            Start: ev.StartTime,
            End: ev.EndTime,
            MaxAttendees: ev.MaxAttendees
            );

        return Results.Ok(response);
    }
}