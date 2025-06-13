using Microsoft.EntityFrameworkCore;
using MusicBackend.Entities;

namespace MusicBackend.Context;

public class MusicDbContext : DbContext
{
    
    public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
    {
    }
    
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistSong> PlaylistSongs { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Download> Downloads { get; set; }

    public DbSet<Follower> Followers { get; set; }  
    
    public DbSet<RecentlyPlayed> RecentlyPlayed { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecentlyPlayed>()
            .HasOne(rp => rp.Song)  
            .WithMany() 
            .HasForeignKey(rp => rp.SongId)  
            .OnDelete(DeleteBehavior.Cascade); 
        
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
        
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "user" },
            new Role { Id = 2, Name = "artist" }
        );
        
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Pop", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Fpopgenre.jpg?alt=media&token=1b5fdd62-54bc-4c15-b206-4ee31210c195", ImageName = "popgenre.jpg"},
            new Genre { Id = 2, Name = "Rap", ImageUrl = "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frapgenre.jpg?alt=media&token=c9ac3e16-f8a0-41b6-a525-a9a826f4e64c", ImageName = "rapgenre.jpg" },
            new Genre { Id = 3, Name = "R&B" ,ImageUrl = "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Genre%2Frbgenre.jpeg?alt=media&token=58bfa6c2-61fe-49c3-9a3f-55a9ee2080b6" , ImageName = "rbgenre.jpeg" }
            
        );
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1, FirstName = "artist1", LastName = "artist1", Email = "artist1@gmail.com",
                DateOfBirth = DateOnly.Parse("2000-01-01"), 
                RoleId = 2, FirebaseId = "VqLmNcrN8dfaEuvCGpRQN0INHaM2"
            },
            new User
            {
                Id = 2, FirstName = "user1", LastName = "user1", Email = "user1@gmail.com",
                DateOfBirth = DateOnly.Parse("2000-01-01"), 
                RoleId = 1, FirebaseId = "EGS0EvbwKpekX3hF01bQXy7Qsdw2"
            }
        );
        
        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "artist1", UserId = 1 ,ImageName = "yh5cdnbnj03",
                ImageUrl = "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Artist%2FProfileImage%2F3%2Fyh5cdnbnj03?alt=media&token=21f75e8d-44a1-4e4e-9cc6-259ead7cef08"}
            
        );

        modelBuilder.Entity<Song>().HasData(
            new Song
            {
                Id = 1,
                Title = "test1song",
                SongUrl = "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Songs%2F1%2Fakow2at5db0?alt=media&token=cd090df9-be52-4b97-88f6-9a7030f9cd33",
                SongName = "akow2at5db0",
                CoverImageUrl = "https://firebasestorage.googleapis.com/v0/b/musicplayer-44afc.appspot.com/o/Songs%2FCoverImages%2F1%2Fbz53xifzunh?alt=media&token=46bad491-5ec5-44d2-8d27-778b367c883d",
                CoverImageName = "bz53xifzunh",
                ReleaseDate = DateOnly.Parse("2002-01-01"),
                ArtistId = 1,
                GenreId = 1,
                AlbumId = null
            }
            );
        
       

    }

   
    
}