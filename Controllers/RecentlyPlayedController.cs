using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Controllers;

public class RecentlyPlayedController : BaseController
{
    public RecentlyPlayedController(MusicDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    [HttpGet("User/{userId}")]
    public async Task<ActionResult<List<SongDto>>> GetUserRecentSongs( int userId)
    {
        try
        {
            var recentSongs = await _context.RecentlyPlayed
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var distinctSongs = recentSongs
                .GroupBy(r => r.SongId)
                .Select(g => g.First())
                .Take(10);
            var songs = new List<Song>();
            foreach (var s in recentSongs)
            {
                var song = await _context.Songs.Include(s=> s.Artist).FirstOrDefaultAsync(x => x.Id == s.SongId);
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
    
    //add song to recently played
    [HttpGet("User/{userId}/Add/{songId}")]
    public async Task<ActionResult> AddToRecentlyPlayed(int userId, int songId)
    {
        
        var newRecentlyPlayed = new RecentlyPlayed
        {
            UserId = userId,
            SongId = songId
        };

        await _context.RecentlyPlayed.AddAsync(newRecentlyPlayed);
        await _context.SaveChangesAsync();

        return Ok();
    }
   
}