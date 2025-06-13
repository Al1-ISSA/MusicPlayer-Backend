using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;
using MusicBackend.Entities;
using MusicBackend.Interfaces;
using MusicBackend.Requests;

namespace MusicBackend.Controllers
{
    public class PlaylistsController : BaseController
    {
        private readonly IFirebaseStorageService _firebaseStorage;
        public PlaylistsController(MusicDbContext context, IMapper mapper, IFirebaseStorageService firebaseStorage) : base(context, mapper)
        {
            _firebaseStorage = firebaseStorage;
        }
      
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetPlaylists()
        {
            var playlists = await _context.Playlists.ToListAsync();
            return _mapper.Map<List<PlaylistDto>>(playlists);
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult<PlaylistDto>> CreatePlaylist([FromBody] CreatePlaylistRequest request)
        {
            try
            {
                
                var playlist = new Playlist
                {
                    Name = request.Name,
                    UserId = request.UserId
                };
                
                _context.Playlists.Add(playlist);
                await _context.SaveChangesAsync();
                return _mapper.Map<PlaylistDto>(playlist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeletePlaylist(int id)
        {
            try
            {
                var playlist = await _context.Playlists.FindAsync(id);
                if (playlist == null)
                {
                    return NotFound();
                }
                //delete all songs in playlist
                var songs = await _context.PlaylistSongs.Where(ps => ps.PlaylistId == id).ToListAsync();
                _context.PlaylistSongs.RemoveRange(songs);
                _context.SaveChanges();
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("Update")]
        public async Task<ActionResult<PlaylistDto>> UpdatePlaylist([FromBody] UpdatePlaylistRequest request)
        {
            try
            {
                var playlist = await _context.Playlists.FindAsync(request.Id);
                if (playlist == null)
                {
                    return NotFound();
                }
                playlist.Name = request.Name;
                await _context.SaveChangesAsync();
                return _mapper.Map<PlaylistDto>(playlist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<PlaylistDto>>> GetPlaylistsByUser(int userId)
        {
            var playlists = await _context.Playlists.Where(p => p.UserId == userId).ToListAsync();
            return _mapper.Map<List<PlaylistDto>>(playlists);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDto>> GetPlaylistById(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return _mapper.Map<PlaylistDto>(playlist);
        }
        
        

    

       

       
    }
}
