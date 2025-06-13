namespace MusicBackend.Requests;

public class UpdateSongRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int GenreId { get; set; }
    public int? AlbumId { get; set; }
}