namespace MusicBackend.Entities;

public class User
{
    public int Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    
    public string FirebaseId { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    
}