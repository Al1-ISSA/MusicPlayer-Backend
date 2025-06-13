using AutoMapper;
using MusicBackend.Dto;
using MusicBackend.Entities;

namespace MusicBackend.Mapper;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDto>();
    }
}