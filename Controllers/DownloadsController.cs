using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Controllers;

public class DownloadsController : BaseController
{
    public DownloadsController(MusicDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    
    [HttpGet("{userId}/AddDownload/{songId}")]
    public async Task<ActionResult> AddDownload(int userId, int songId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(songId);
            if (song == null)
            {
                return NotFound();
            }

            var download = new Download
            {
                UserId = userId,
                SongId = songId
            };
            
            _context.Downloads.Add(download);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{userId}/RemoveDownload/{songId}")]
    public async Task<ActionResult> RemoveDownload(int userId, int songId)
    {
        try
        {
            var download = await _context.Downloads.FirstOrDefaultAsync(x => x.UserId == userId && x.SongId == songId);
            if (download == null)
            {
                return NotFound();
            }

            _context.Downloads.Remove(download);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("User/{userId}")]
    public async Task<ActionResult<List<SongDto>>> GetDownloads(int userId)
    {
        try
        {
            var downloads = await _context.Downloads.Where(x => x.UserId == userId).ToListAsync();
            var songs = new List<Song>();
            foreach (var download in downloads)
            {
                var song = await _context.Songs.FindAsync(download.SongId);
                if (song != null)
                {
                    songs.Add(song);
                }
            }

            return Ok(_mapper.Map<List<SongDto>>(songs));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}