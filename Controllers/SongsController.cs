using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;
using MusicBackend.Interfaces;
using MusicBackend.Requests;
using NAudio.Wave;

namespace MusicBackend.Controllers;

public class SongsController : BaseController
{

    private readonly IFirebaseStorageService _firebaseStorage;
    public SongsController(MusicDbContext context, IMapper mapper, IFirebaseStorageService firebaseStorageService) : base(context, mapper)
    {
        _firebaseStorage = firebaseStorageService;
    }
  
    
    [HttpPost("Upload")]
    public async Task<ActionResult<SongDto>> UploadSong([FromForm] UploadSongRequest request)
    {
        try
        {   // upload song file to firebase storage
            var songName = Path.GetRandomFileName().Replace(".", "");
            var folderName = "Songs/" + request.ArtistId;
            var downloadUrl = await _firebaseStorage.SaveFileToCloudAsync(request.SongFile, songName,folderName );
            
            
            //upload cover image to firebase storage
            var coverImageName = Path.GetRandomFileName().Replace(".", "");
            var coverImageFolderName = "Songs/CoverImages/" + request.ArtistId;
            var coverImageUrl = await _firebaseStorage.SaveFileToCloudAsync(request.CoverImage, coverImageName,coverImageFolderName );
            
            var song = new Song
            {
                Title = request.Title,
                CoverImageUrl = coverImageUrl,
                CoverImageName = coverImageName,
                ReleaseDate = request.ReleaseDate,
                SongUrl = downloadUrl,
                SongName = songName,
                ArtistId = request.ArtistId,
                GenreId = request.GenreId,
                AlbumId = request.AlbumId
            };
            
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<SongDto>(song);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
   
    
    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteSong(int id)
    {
        try
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            // delete song file from firebase storage
            await _firebaseStorage.DeleteFileFromCloudAsync(song.SongName, "Songs/" + song.ArtistId);
            await _firebaseStorage.DeleteFileFromCloudAsync(song.CoverImageName, "Songs/CoverImages/" + song.ArtistId);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("Update")]
    public async Task<ActionResult<SongDto>> UpdateSong([FromForm] UpdateSongRequest request)
    {
        try
        {
            var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (song == null)
            {
                return NotFound();
            }
            
            song.Title = request.Title;
            song.ReleaseDate = request.ReleaseDate;
            song.GenreId = request.GenreId;
            song.AlbumId = request.AlbumId;
            
            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<SongDto>(song);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("Search")]
    public async Task<ActionResult<SearchDto>> SearchSongs(string searchQuery)
    {
        var songs = await _context.Songs.Include(a => a.Artist).Where(x => x.Title.Contains(searchQuery)).ToListAsync();
        var artists = await _context.Artists.Where(x => x.Name.Contains(searchQuery)).ToListAsync();
        var albums = await _context.Albums.Where(x => x.Title.Contains(searchQuery)).ToListAsync();
        
        var searchDto = new SearchDto
        {
            Songs = _mapper.Map<List<SongDto>>(songs),
            Artists = _mapper.Map<List<ArtistDto>>(artists),
            Albums = _mapper.Map<List<AlbumDto>>(albums)
        };
        
        return searchDto;
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<SongDto>> GetSong(int id)
    {
        var song = await _context.Songs.Include(a => a.Artist).FirstOrDefaultAsync(x => x.Id == id);
        if (song == null)
        {
            return NotFound();
        }
        return _mapper.Map<SongDto>(song);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<SongDto>>> GetAllSongs()
    {
        var songs = await _context.Songs.Include(a => a.Artist).ToListAsync();
        return Ok(_mapper.Map<List<SongDto>>(songs));
    }
    
    
    [HttpGet("Artist/{artistId}")]
    public async Task<ActionResult<ArtistPageDto>> GetSongsByArtistAndAlbum(int artistId)
    {
        var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == artistId);
        var songs = await _context.Songs.Include(a => a.Artist).Where(x => x.ArtistId == artistId && x.AlbumId == null).ToListAsync();
        
        var artistPage = new ArtistPageDto
        {
            Artist = _mapper.Map<ArtistDto>(artist),
            Songs = _mapper.Map<List<SongDto>>(songs)
        };
        
        return artistPage;
        
    }
    
    [HttpGet("Album/{albumId}")]
    public async Task<ActionResult<List<SongDto>>> GetSongsByAlbum(int albumId)
    {
        var album = await _context.Albums.FirstOrDefaultAsync(x => x.Id == albumId);
        var songs = await _context.Songs.Include(a => a.Artist).Where(x => x.AlbumId == albumId).ToListAsync();
      
        return Ok(_mapper.Map<List<SongDto>>(songs));
        
    }
    
    [HttpGet("Genre/{genreId}")]
    public async Task<ActionResult<List<SongDto>>> GetSongsByGenre(int genreId)
    {
        var songs = await _context.Songs.Include(a => a.Artist).Where(x => x.GenreId == genreId).ToListAsync();
        return Ok(_mapper.Map<List<SongDto>>(songs));
    }
    
    //update view count of song
    [HttpGet("ViewCount/{songId}")]
    public async Task<ActionResult> UpdateViewCount(int songId)
    {
        var song = await _context.Songs.FirstOrDefaultAsync(x => x.Id == songId);
        if (song == null)
        {
            return NotFound();
        }
        song.ViewCount++;
        _context.Songs.Update(song);
        await _context.SaveChangesAsync();
        return Ok();
    }


    
}