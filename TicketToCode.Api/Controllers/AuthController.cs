using Microsoft.AspNetCore.Mvc;
using TicketToCode.Api.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) 
    {
        _authService = authService;
    }

    [HttpPost("login")]
public IActionResult Login([FromBody] LoginModel model)
{
    if (model == null)
    {
        return BadRequest("Invalid login request.");
    }

    var user = _authService.Login(model.Username, model.Password, out string token);
    if (user == null) 
    {
        return Unauthorized("Felaktigt användarnamn eller lösenord.");
    }

    return Ok(new { Token = token, Role = user.Role });
}

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
         var user = _authService.Register(model.Username, model.Password, UserRoles.User); 
        if (user == null)
        {
            return BadRequest(new { message = "Användarnamnet är redan taget" });
        }

        return Ok(new { message = "Registrering lyckades!" });
    }
}

public class LoginModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class RegisterModel
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
