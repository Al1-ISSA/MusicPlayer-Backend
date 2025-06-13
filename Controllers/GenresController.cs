using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Dto;

namespace MusicBackend.Controllers;

public class GenresController : BaseController
{
    public GenresController(
        MusicDbContext context,
        IMapper mapper
    ) : base(context, mapper)
    {
    }

    [HttpGet]
    public async Task<IEnumerable<GenreDto>> GetGenres()
    {
        var genres = await _context.Genres
            .ToListAsync();

        return _mapper.Map<IEnumerable<GenreDto>>(genres);
    }
}