using TicketToCode.Api.Endpoints;
using TicketToCode.Api.Services;
using Microsoft.EntityFrameworkCore;
using TicketToCode.Core.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Ladda in PostgreSQL-anslutning från `appsettings.json`
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Lägg till Entity Framework Core med PostgreSQL
builder.Services.AddDbContext<TicketToCodeDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSingleton<IDatabase, Database>();
builder.Services.AddScoped<IAuthService, AuthService>();

// 🔹 Lägg till Controllers (för API-endpoints)
builder.Services.AddControllers();

// 🔹 Lägg till CORS för att låta Blazor anropa API:t
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// 🔹 Lägg till autentisering med cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

var app = builder.Build();

// 🔹 Aktivera CORS före autentisering
app.UseCors("AllowBlazor");

// 🔹 Konfigurera utvecklingsläget (Swagger)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Gör så att API:t hittar Controllers och Endpoints
app.MapControllers(); // 👈 Lägger till stöd för Controllers
app.MapEndpoints<Program>(); // 👈 Lägger till dina API-endpoints från `Endpoints/`

app.Run();
