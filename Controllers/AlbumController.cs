using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;
using MusicBackend.Interfaces;
using MusicBackend.Requests;

namespace MusicBackend.Controllers;

public class AlbumController : BaseController
{
    
    private readonly IFirebaseStorageService _firebaseStorage;
    public AlbumController(MusicDbContext context, IMapper mapper, IFirebaseStorageService firebaseStorage) : base(context, mapper)
    {
        _firebaseStorage = firebaseStorage;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbums()
    {
        var albums = await _context.Albums.ToListAsync();
        return _mapper.Map<List<AlbumDto>>(albums);
    }
    
    [HttpPost("Create")]
    public async Task<ActionResult<AlbumDto>> CreateAlbum([FromForm] CreateAlbumRequest request)
    {
        try
        {
            //upload cover image to firebase storage
            var coverImageName = Path.GetRandomFileName().Replace(".", "");
            var coverImageFolderName = "Album/CoverImages/" + request.ArtistId;
            var coverImageUrl = await _firebaseStorage.SaveFileToCloudAsync(request.CoverImage, coverImageName,coverImageFolderName );

            
            var album = new Album
            {
                Title = request.Title,
                ReleaseDate = request.ReleaseDate,
                CoverImageUrl = coverImageUrl,
                CoverImageName = coverImageName ,
                ArtistId = request.ArtistId
            };
            
            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return _mapper.Map<AlbumDto>(album);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost("Update")]
    public async Task<ActionResult<AlbumDto>> UpdateAlbum([FromForm] UpdateAlbumRequest request)
    {
        try
        {
            var album = await _context.Albums.FindAsync(request.Id);
            if (album == null)
            {
                return NotFound();
            }
            if (request.CoverImage != null)
            {
                //delete old cover image from firebase storage
                await _firebaseStorage.DeleteFileFromCloudAsync(album.CoverImageName, "Album/CoverImages/" + album.ArtistId);
                
                var coverImageName = Path.GetRandomFileName().Replace(".", "");
                var coverImageFolderName = "Album/CoverImages/" + album.ArtistId;
                var coverImageUrl = await _firebaseStorage.SaveFileToCloudAsync(request.CoverImage, coverImageName,coverImageFolderName );
                album.CoverImageUrl = coverImageUrl;
                album.CoverImageName = coverImageName;
            }
            album.Title = request.Title;
            album.ReleaseDate = request.ReleaseDate;

            await _context.SaveChangesAsync();
            return _mapper.Map<AlbumDto>(album);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("Delete")]
    public async Task<ActionResult> DeleteAlbum(int id)
    {
        try
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            //delete all songs in album
            var songs = await _context.Songs.Where(x => x.AlbumId == id).ToListAsync();
            foreach (var song in songs)
            {
                await _firebaseStorage.DeleteFileFromCloudAsync(song.SongName, "Songs/" + song.ArtistId);
                await _firebaseStorage.DeleteFileFromCloudAsync(song.CoverImageName, "Songs/CoverImages/" + song.ArtistId);
                _context.Songs.Remove(song);
            }
            //delete cover image from firebase storage
            await _firebaseStorage.DeleteFileFromCloudAsync(album.CoverImageName, "Album/CoverImages/" + album.ArtistId);

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AlbumDto>> GetAlbum(int id)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album == null)
        {
            return NotFound();
        }
        return _mapper.Map<AlbumDto>(album);
    }
    
    [HttpGet("Artist/{artistId}")]
    public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAlbumsByArtist(int artistId)
    {
        var albums = await _context.Albums.Where(x => x.ArtistId == artistId).ToListAsync();
        return _mapper.Map<List<AlbumDto>>(albums);
    }
    
    
    

    
    
}