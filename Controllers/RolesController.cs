using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;

namespace MusicBackend.Controllers;


public class RolesController : BaseController
{
    public RolesController(
        MusicDbContext context,
        IMapper mapper
    ) : base(context, mapper)
    {
    }
    [HttpGet]
    public async Task<IEnumerable<RoleDto>> GetRoles()
    {
        var roles = await _context.Roles
            .ToListAsync();

        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}
    
