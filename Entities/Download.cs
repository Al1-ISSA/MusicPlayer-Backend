namespace MusicBackend.Entities;

public class Download
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SongId { get; set; }
}