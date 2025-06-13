namespace MusicBackend.Entities;

public class Like
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SongId { get; set; }
}