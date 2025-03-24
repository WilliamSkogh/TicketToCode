using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TicketToCode.Core.Data;
using TicketToCode.Core.Models;
using Microsoft.Extensions.Configuration;

namespace TicketToCode.Api.Services;

public interface IAuthService
{
    User? Login(string username, string password, out string token);
    User? Register(string username, string password, string role);
}

public class AuthService : IAuthService
{
    private readonly TicketToCodeDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public AuthService(TicketToCodeDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public User? Login(string username, string password, out string token)
    {
        token = string.Empty; 

        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        token = GenerateJwtToken(user);
        return user;
    }

    public User? Register(string username, string password, string role)
    {
        if (_dbContext.Users.Any(u => u.Username == username))
        {
            return null;
        }

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    private string GenerateJwtToken(User user)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT SecretKey is missing from configuration.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

        };

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(24), 
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}