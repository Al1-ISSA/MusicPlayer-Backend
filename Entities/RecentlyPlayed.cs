namespace MusicBackend.Entities;

public class RecentlyPlayed
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SongId { get; set; }
    public virtual Song Song { get; set; }
    public virtual User User { get; set; }
}