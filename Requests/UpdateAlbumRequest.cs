namespace MusicBackend.Requests;

public class UpdateAlbumRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IFormFile CoverImage { get; set; }
    public DateOnly ReleaseDate { get; set; }

}