using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Controllers;

public class PlaylistSongsController : BaseController
{
    public PlaylistSongsController(MusicDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    
    [HttpPost("AddSong")]
    public async Task<ActionResult> AddSongToPlaylist(int playlistId, int songId)
    {
        try
        {
            var playlist = await _context.Playlists.FindAsync(playlistId);
            if (playlist == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(songId);
            if (song == null)
            {
                return NotFound();
            }

            var playlistSong = new PlaylistSong
            {
                PlaylistId = playlistId,
                SongId = songId
            };
            
            _context.PlaylistSongs.Add(playlistSong);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{playlistId}/RemoveSong/{songId}")]
    public async Task<ActionResult> RemoveSongFromPlaylist(int playlistId, int songId)
    {
        try
        {
            var playlistSong = await _context.PlaylistSongs.FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.SongId == songId);
            if (playlistSong == null)
            {
                return NotFound();
            }

            _context.PlaylistSongs.Remove(playlistSong);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{playlistId}/Songs")]
    public async Task<ActionResult<List<SongDto>>> GetSongsFromPlaylist(int playlistId)
    {
        try
        {
            var playlistSongs = await _context.PlaylistSongs.Where(x => x.PlaylistId == playlistId).ToListAsync();
            var songs = new List<Song>();
            foreach (var playlistSong in playlistSongs)
            {
                var song = await _context.Songs.Include(a => a.Artist).FirstOrDefaultAsync(x => x.Id == playlistSong.SongId);
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