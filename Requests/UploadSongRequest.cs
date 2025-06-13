namespace MusicBackend.Requests;

public class UploadSongRequest
{
    public IFormFile SongFile { get; set; }
    public string Title { get; set; }
    public IFormFile CoverImage { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public int GenreId { get; set; }
    public int? AlbumId { get; set; }


}