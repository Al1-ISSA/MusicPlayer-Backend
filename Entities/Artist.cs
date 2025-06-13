namespace MusicBackend.Entities;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int UserId { get; set; }
    public virtual User User { get; set; }
    
    public string? ImageUrl { get; set; }
    public string? ImageName { get; set; }
}