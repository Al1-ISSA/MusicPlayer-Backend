using MusicBackend.Entities;

namespace MusicBackend.Dto;

public class ArtistDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string ImageName { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; }
    
}