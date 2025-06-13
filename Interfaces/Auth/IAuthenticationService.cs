namespace MusicBackend.Interfaces.Auth;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(string email, string password);
}