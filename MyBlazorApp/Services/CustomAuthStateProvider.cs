using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public CustomAuthStateProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
{
    var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

    if (string.IsNullOrWhiteSpace(token) || token.Split('.').Length != 3)
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    var user = new ClaimsPrincipal(identity);
    return new AuthenticationState(user);
}

    public async Task SetUserAsync(string token)
{
    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", token);

    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);

    var claims = jwtToken.Claims;
    var identity = new ClaimsIdentity(claims, "jwt");

    _currentUser = new ClaimsPrincipal(identity);
    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}


    public async Task Logout()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.Claims;
    }
}