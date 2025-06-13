namespace MusicBackend.Requests;

public class ArtistRegistrationRequest
{
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Role { get; set; }
    
    public string Name { get; set; }
    public IFormFile Image { get; set; }
}