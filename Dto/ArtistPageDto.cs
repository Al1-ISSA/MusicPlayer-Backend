namespace MusicBackend.Dto;

public class ArtistPageDto
{
    public ArtistDto Artist { get; set; }
    public List<SongDto> Songs { get; set; }
    
    public List<AlbumDto>? Albums { get; set; }
}