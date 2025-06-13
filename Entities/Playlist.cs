namespace MusicBackend.Entities;

public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? CoverImage { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; }
}