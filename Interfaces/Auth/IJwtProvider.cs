namespace MusicBackend.Interfaces.Auth;

public interface IJwtProvider
{
    Task<string> GetForCredentialsAsync(string email, string password,string role,long userId);
}