using AutoMapper;

namespace MusicBackend.Mapper;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<Entities.Artist, Dto.ArtistDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
    
}