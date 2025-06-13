using FirebaseAdmin.Auth;
using MusicBackend.Interfaces.Auth;

namespace MusicBackend.Services.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public JwtProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

    }
    public async Task<string> GetForCredentialsAsync(string email, string password,string role,long userId)
    {
        var request = new
        {
            email,
            password,
            returnSecureToken = true,
        };
        
        var response = await _httpClient.PostAsJsonAsync("", request);
        
        var authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        
        if (authToken == null)
        {
            throw new Exception("Invalid email or password");
        }
        
        var additionalClaims = new Dictionary<string, object>()
        {
            {"Email", email},
            { "Role", role },
            { "UserId", userId },
        };
        
        // var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(authToken.LocalId, additionalClaims);
        await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(authToken.LocalId, additionalClaims);
        return authToken.IdToken;

        // return customToken;
    }
}