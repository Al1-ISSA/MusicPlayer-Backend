namespace MusicBackend.Requests;

public class CreateAlbumRequest
{
    public string Title { get; set; }
    public IFormFile CoverImage { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public int ArtistId { get; set; }

}