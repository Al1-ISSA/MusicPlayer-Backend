namespace MusicBackend.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string? ImageUrl { get; set; }
    public string? ImageName { get; set; }
}