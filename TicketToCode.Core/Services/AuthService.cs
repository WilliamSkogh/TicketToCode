using TicketToCode.Core.Data;
using TicketToCode.Core.Models;

namespace TicketToCode.Api.Services;

public interface IAuthService
{
    User? Login(string username, string password);
    User? Register(string username, string password);
}

// TODO: Implement better auth
/// <summary>
/// Simple auth service to enable registering and login in, should be replaced before release
/// </summary>
public class AuthService : IAuthService
{
    private readonly TicketToCodeDbContext _dbContext;

    public AuthService(TicketToCodeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? Login(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        return new User(user.Username, user.Role);
    }

    public User? Register(string username, string password)
    {
        if (_dbContext.Users.Any(u => u.Username == username))
        {
            return null;
        }

        var user = new User(username, BCrypt.Net.BCrypt.HashPassword(password));

        _dbContext.Users.Add(user);
        return user;
    }
} 