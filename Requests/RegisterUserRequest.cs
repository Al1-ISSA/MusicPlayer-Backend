namespace MusicBackend.Requests;

public class RegisterUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Role { get; set; }
}
