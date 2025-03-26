// ✅ Program.cs för MyBlazorApp (Blazor WebAssembly) – med enum-fix
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using MyBlazorApp;
using MyBlazorApp.Services;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 🔐 Auth setup
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>());

// 🔌 HttpClient för API-anrop
builder.Services.AddHttpClient("TicketToCode.ServerAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5235"); // byt till https om du använder det
});

// HttpClient med enum-support (JsonStringEnumConverter)
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IHttpClientFactory>().CreateClient("TicketToCode.ServerAPI");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    return client;
});

// 📦 Enum-serialisering i hela appen
builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new JsonStringEnumConverter());
});


// 🛠 Dina egna tjänster
builder.Services.AddScoped<EventService>();

await builder.Build().RunAsync();
