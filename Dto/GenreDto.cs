namespace MusicBackend.Dto;

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? ImageName { get; set; }
}