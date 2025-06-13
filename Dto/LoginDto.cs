namespace MusicBackend.Dto;

public class LoginDto
{
    public string Token { get; set; }
    public int UserId { get; set; }
    public string Role { get; set; }
    public int? ArtistId { get; set; }
}