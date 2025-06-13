using AutoMapper;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Mapper;

public class AlbumProfile : Profile
{
    
    public AlbumProfile()
    {
        CreateMap<Album, AlbumDto>();

    }
    
    
    
}