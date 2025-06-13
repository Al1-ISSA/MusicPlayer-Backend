namespace MusicBackend.Dto;

public class AlbumDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string CoverImageUrl { get; set; }
    public string CoverImageName { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int ArtistId { get; set; }

}