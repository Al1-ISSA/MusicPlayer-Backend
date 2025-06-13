using AutoMapper;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Mapper;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
    }
    
}