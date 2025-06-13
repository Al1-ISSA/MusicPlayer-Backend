using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicBackend.Context;
using MusicBackend.Context.Extensions;
using MusicBackend.Dto;
using MusicBackend.Interfaces;
using MusicBackend.Interfaces.Auth;
using MusicBackend.Requests;


namespace MusicBackend.Controllers;

public class UsersController : BaseController
{
    private readonly IFirebaseStorageService _firebaseStorage;
    private readonly IAuthenticationService _authenticationService;
    private readonly IJwtProvider _jwtProvider;
    
    public UsersController(
        MusicDbContext context, 
        IMapper mapper,
        IAuthenticationService authenticationService, 
        IJwtProvider jwtProvider, IFirebaseStorageService firebaseStorage) : base(context, mapper) 
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        _firebaseStorage = firebaseStorage;
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] RegisterUserRequest request)
    {
        try
        {
            var role = await _context.Roles.CheckEntityExistenceAsync(r => r.Name == request.Role, request.Role);

            await _context.Users.CheckEntityAlreadyExistsAsync(u => u.Email == request.Email, request.Email, "Email");

            var firebaseId = await _authenticationService.RegisterAsync(request.Email, request.Password);

            var entity = new Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                RoleId = role.Id,
                Role = role,
                FirebaseId = firebaseId,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("Register/Artist")]
    public async Task<ActionResult<UserDto>> CreateArtist([FromForm] ArtistRegistrationRequest request)
    {
        try
        {
            var role = await _context.Roles.CheckEntityExistenceAsync(r => r.Name == request.Role, request.Role);

            await _context.Users.CheckEntityAlreadyExistsAsync(u => u.Email == request.Email, request.Email, "Email");

            var firebaseId = await _authenticationService.RegisterAsync(request.Email, request.Password);

            var entity = new Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                RoleId = role.Id,
                Role = role,
                FirebaseId = firebaseId,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            
            var coverImageName = Path.GetRandomFileName().Replace(".", "");
            var coverImageFolderName = "Artist/ProfileImage/" + entity.Id;
            var coverImageUrl = await _firebaseStorage.SaveFileToCloudAsync(request.Image, coverImageName,coverImageFolderName );

            var artist = new Entities.Artist
            {
                Name = request.Name,
                ImageUrl = coverImageUrl,
                ImageName = coverImageName,
                UserId = entity.Id
            };
            
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(entity);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<LoginDto>> LoginUser([FromBody]LoginUserRequest request)
    {
        try
        {

            var user = await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }

            var token = await _jwtProvider.GetForCredentialsAsync(request.Email, request.Password, user.Role.Name, user.Id);
            
            if (user.Role.Name == "artist")
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(x => x.UserId == user.Id);
                return new LoginDto
                {
                    Token = token,
                    UserId = user.Id,
                    Role = user.Role.Name,
                    ArtistId = artist.Id
                };
            }
            
            return new LoginDto
            {
                Token = token,
                UserId = user.Id,
                Role = user.Role.Name,
                ArtistId = null
            };
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _context.Users
            .Include(r => r.Role)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<UserDto>>(users);
        
    }
}