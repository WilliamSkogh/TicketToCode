<<<<<<< HEAD
=======
using TicketToCode.Api.Endpoints;
using TicketToCode.Api.Services;
using Microsoft.EntityFrameworkCore;
using TicketToCode.Core.Data;

>>>>>>> main
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOpenApi();
<<<<<<< HEAD
=======

// PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TicketToCodeDbContext>(options =>
    options.UseNpgsql(connectionString));


>>>>>>> main
builder.Services.AddSingleton<IDatabase, Database>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add cookie authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

// ✅ Flytta CORS-konfigurationen hit
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// ✅ Flytta `UseCors` HIT, före autentisering och endpoints
app.UseCors("AllowBlazor");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.DefaultModelsExpandDepth(-1);
    });
}

// Map all endpoints
app.MapEndpoints<Program>();

app.Run();
