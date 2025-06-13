using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Controllers;

public class FollowersController : BaseController
{
    public FollowersController(MusicDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    //add follower to user
    [HttpGet("{userId}/AddFollower/{artistId}")]
    public async Task<ActionResult> AddFollower(int userId, int artistId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var follower = await _context.Artists.FindAsync(artistId);
            if (follower == null)
            {
                return NotFound();
            }
            
            //check if user is already following artist
            var isFollowing = await _context.Followers.Where(x => x.UserId == userId && x.ArtistId == artistId).FirstOrDefaultAsync();
            if (isFollowing != null)
            {
                return BadRequest("User is already following artist");
            }
            
            var follow = new Follower
            {
                UserId = userId,
                ArtistId = artistId
            };
            
            _context.Followers.Add(follow);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //remove follower from user
    [HttpDelete("{userId}/RemoveFollower/{artistId}")]
    public async Task<ActionResult> RemoveFollower(int userId, int artistId)
    {
        try
        {
            var follow = await _context.Followers.FirstOrDefaultAsync(x => x.UserId == userId && x.ArtistId == artistId);
            if (follow == null)
            {
                return NotFound();
            }
            
            _context.Followers.Remove(follow);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //get all artists followed by user
    [HttpGet("User/{userId}")]
    public async Task<ActionResult<List<ArtistDto>>> GetFollowedArtists(int userId)
    {
        try
        {
            var following = await _context.Followers.Where(x => x.UserId == userId).ToListAsync();
            var artists = new List<Artist>();
            foreach (var f in following)
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(x => x.Id == f.ArtistId);
                if (artist != null)
                {
                    artists.Add(artist);
                }
                
            }

            return Ok(_mapper.Map<List<ArtistDto>>(artists));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    //check if user is following artist
    [HttpGet("{userId}/IsFollowing/{artistId}")]
    public async Task<ActionResult<bool>> IsFollowing(int userId, int artistId)
    {
        var isFollowing = await _context.Followers.Where(x => x.UserId == userId && x.ArtistId == artistId).FirstOrDefaultAsync();
        return Ok(isFollowing != null);
    }

    //get artist follower count
    [HttpGet("Artist/{artistId}")]
    public async Task<ActionResult<int>> GetFollowerCount(int artistId)
    {
        var count = await _context.Followers.Where(x => x.ArtistId == artistId).CountAsync();
        return Ok(count);
    }
    
    
}