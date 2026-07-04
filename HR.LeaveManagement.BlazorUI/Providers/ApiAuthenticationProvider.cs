using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace HR.LeaveManagement.BlazorUI.Providers;

public class ApiAuthenticationProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;


    public ApiAuthenticationProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var Identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(Identity);
        var isTokenPrisent = await _localStorage.ContainKeyAsync("token");
        if(isTokenPrisent == false)
        {
            return new AuthenticationState(user);
        }

        var savedToken = await _localStorage.GetItemAsync<string>("token");
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

        if(tokenContent.ValidTo < DateTime.UtcNow)
        {
            await _localStorage.RemoveItemAsync("token");
            return new AuthenticationState(user);
        }

        var claims = await GetClaims();

        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);

    }

    public async Task LoggedIn()
    {
        var claims = await GetClaims();
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync("token");
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var token = await _localStorage.GetItemAsync<string>("token");
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(token);
        var claims = tokenContent.Claims.ToList();
        
        return claims;
    }
}
