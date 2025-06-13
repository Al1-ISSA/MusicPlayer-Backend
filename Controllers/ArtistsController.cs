using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Context.Extensions;
using MusicBackend.Dto;
using MusicBackend.Interfaces;
using MusicBackend.Requests;

namespace MusicBackend.Controllers;

public class ArtistsController : BaseController
{
    private readonly IFirebaseStorageService _firebaseStorage;
    public ArtistsController(
        MusicDbContext context,
        IMapper mapper, IFirebaseStorageService firebaseStorage) : base(context, mapper)
    {
        _firebaseStorage = firebaseStorage;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ArtistDto>> GetArtist(int id)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<ArtistDto>(artist);
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<ArtistDto>> CreateArtist([FromForm] RegisterArtistRequest request)
    {
        try
        {
            var checkUser = await _context.Users.Include(r=> r.Role).CheckEntityExistenceAsync(u => u.Id == request.UserId && u.Role.Name == "artist" , request.UserId);
            

            var coverImageName = Path.GetRandomFileName().Replace(".", "");
            var coverImageFolderName = "Artist/ProfileImage/" + request.UserId;
            var coverImageUrl = await _firebaseStorage.SaveFileToCloudAsync(request.Image, coverImageName,coverImageFolderName );

            var entity = new Entities.Artist
            {
                Name = request.Name,
                ImageUrl = coverImageUrl,
                ImageName = coverImageName,
                UserId = request.UserId
            };

            _context.Artists.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<ArtistDto>(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IEnumerable<ArtistDto>> GetArtists()
    {
        var artists = await _context.Artists
            .Include(a => a.User)
            .ThenInclude(a => a.Role)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ArtistDto>>(artists);
    }
    
    [HttpGet("Profile/{artistId}")]
    public async Task<ActionResult<ArtistPageDto>> GetArtistPage(int artistId)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == artistId);
        var songs = await _context.Songs.Where(x => x.ArtistId == artistId && x.AlbumId == null).ToListAsync();
        var albums = await _context.Albums.Where(x => x.ArtistId == artistId).ToListAsync();
        
        var artistPage = new ArtistPageDto
        {
            Artist = _mapper.Map<ArtistDto>(artist),
            Songs = _mapper.Map<List<SongDto>>(songs),
            Albums =_mapper.Map<List<AlbumDto>>(albums),
        };
        
        return artistPage;
        
    }
    
}