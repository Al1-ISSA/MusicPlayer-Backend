namespace MusicBackend.Entities;

public class Song
{
    public Song()
    {
        ViewCount = 0;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string SongUrl { get; set; }
    public string SongName { get; set; }
    public string CoverImageUrl { get; set; }
    public string CoverImageName { get; set; } 
    public DateOnly ReleaseDate { get; set; }
    
    public int ArtistId { get; set; }
    public virtual Artist Artist { get; set; }
    
    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; }
    
    public int? AlbumId { get; set; }
    public virtual Album? Album { get; set; }
    
    public int? ViewCount { get; set; }
}