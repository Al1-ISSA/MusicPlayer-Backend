using FirebaseAdmin.Auth;
using MusicBackend.Interfaces.Auth;

namespace MusicBackend.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    public async Task<string> RegisterAsync(string email, string password)
    {
        
        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs()
        {
            Email = email,
            Password = password
        });
        
        if (userRecord == null)
        {
            throw new Exception("User creation failed");
        }
        
        return userRecord.Uid;

    }
}