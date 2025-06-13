namespace MusicBackend.Dto;

public class SearchDto
{
    public List<SongDto>? Songs { get; set; }
    public List<ArtistDto>? Artists { get; set; }
    public List<AlbumDto>? Albums { get; set; }
}