using AutoMapper;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Mapper;

public class SongProfile : Profile
{
    
    public SongProfile()
    {
        CreateMap<Song, SongDto>();

    }
    
}