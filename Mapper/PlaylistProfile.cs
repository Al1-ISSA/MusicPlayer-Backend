using AutoMapper;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Mapper;

public class PlaylistProfile : Profile
{
    public PlaylistProfile()
    {
        CreateMap<Playlist, PlaylistDto>();
        
    }
    
}