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
        return await _httpClient.GetFromJsonAsync<List<Event>>("api/events");
    }

    public async Task<bool> CreateEventAsync(Event newEvent)
    {
        var response = await _httpClient.PostAsJsonAsync("api/events", newEvent);
        return response.IsSuccessStatusCode;
    }
}