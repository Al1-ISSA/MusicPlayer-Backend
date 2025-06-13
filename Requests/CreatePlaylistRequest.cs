namespace MusicBackend.Requests;

public class CreatePlaylistRequest
{
    public string Name { get; set; }
    public int UserId { get; set; }
}