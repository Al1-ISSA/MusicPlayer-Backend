namespace MusicBackend.Requests;

public class RegisterArtistRequest
{
    public string Name { get; set; }
    public int UserId { get; set; }
    
    public IFormFile Image { get; set; }
}