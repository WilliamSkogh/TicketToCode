using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public AuthController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var loginData = new
        {
            client_id = "blazor-client",
            grant_type = "password",
            username = model.Username,
            password = model.Password
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5001/connect/token", loginData);

        if (!response.IsSuccessStatusCode)
        {
            return Unauthorized(new { message = "Felaktigt användarnamn eller lösenord" });
        }

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return Ok(result);
    }

  
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userData = new
        {
            username = model.Username,
            password = model.Password
        };

        var response = await _httpClient.PostAsJsonAsync("http://localhost:5001/api/register", userData);

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest(new { message = "Kunde inte skapa användaren" });
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


public class TokenResponse
{
    public required string Access_Token { get; set; }
    public required string Token_Type { get; set; }
    public int Expires_In { get; set; }
}
