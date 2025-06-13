using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicBackend.Context;

namespace MusicBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected readonly MusicDbContext _context;
    protected readonly IMapper _mapper;


    protected BaseController(MusicDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}