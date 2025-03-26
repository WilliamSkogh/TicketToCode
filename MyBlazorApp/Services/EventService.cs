using System.Net.Http.Json;
using TicketToCode.Core.Models;

namespace MyBlazorApp.Services;

public class EventService
{
    private readonly HttpClient _httpClient;

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Event>?> GetEventsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Event>>("/events");
    }

    public async Task<bool> CreateEventAsync(Event newEvent)
    {
        var response = await _httpClient.PostAsJsonAsync("/events", newEvent);
        return response.IsSuccessStatusCode;
    }
    public async Task<HttpResponseMessage> CreateEventRawAsync(Event newEvent)
    {
        return await _httpClient.PostAsJsonAsync("/events", newEvent);
    }
    public async Task<HttpResponseMessage> DeleteEventAsync(int id)
{
    return await _httpClient.DeleteAsync($"api/events/{id}");
}

public async Task<List<TicketsSoldDto>> GetTicketsSoldPerDay()
{
    var result = await _httpClient.GetFromJsonAsync<List<TicketsSoldDto>>("http://localhost:5235/api/ticket/sold-by-day");
    return result ?? new List<TicketsSoldDto>();
}

public class TicketsSoldDto
{
    public string Day { get; set; } = string.Empty;
    public int SoldTickets { get; set; }
}
}