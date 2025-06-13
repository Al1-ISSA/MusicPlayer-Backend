using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Controllers;

public class LikesController : BaseController
{
    public LikesController(MusicDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    //add song to user's liked songs
    [HttpGet("{userId}/AddLike/{songId}")]
    public async Task<ActionResult> AddLike(int userId, int songId)
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
            
            //check if user already liked song
            var isLiked = await _context.Likes.Where(x => x.UserId == userId && x.SongId == songId).FirstOrDefaultAsync();
            if (isLiked != null)
            {
                return BadRequest("User already liked song");
            }

            var like = new Like
            {
                UserId = userId,
                SongId = songId
            };
            
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //remove song from user's liked songs
    [HttpDelete("{userId}/RemoveLike/{songId}")]
    public async Task<ActionResult> RemoveLike(int userId, int songId)
    {
        try
        {
            var like = await _context.Likes.FirstOrDefaultAsync(x => x.UserId == userId && x.SongId == songId);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //get all songs liked by user
    [HttpGet("User/{userId}")]
    public async Task<ActionResult<List<SongDto>>> GetLikedSongs(int userId)
    {
        try
        {
            var likes = await _context.Likes.Where(x => x.UserId == userId).ToListAsync();
            var songs = new List<Song>();
            foreach (var like in likes)
            {
                var song = await _context.Songs.Include(a => a.Artist).FirstOrDefaultAsync(x => x.Id == like.SongId);
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
    
    
    //check if song is liked by user
    [HttpGet("{userId}/IsLiked/{songId}")]
    public async Task<ActionResult<bool>> IsLiked(int userId, int songId)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(x => x.UserId == userId && x.SongId == songId);
        return Ok(like != null);
    }
}