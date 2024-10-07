using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Interface;
using DEMO_ASP_.NET_CORE_Web_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEMO_ASP_.NET_CORE_Web_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) 
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName);

        if (user == null) return Unauthorized("Invalid username!");

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if(!result.Succeeded) return Unauthorized("Invalid username or password!");
        CreateUserResponseDto response = new CreateUserResponseDto
        {
            UserName = user.UserName,
            UserEmail = user.Email,
            AccessToken = _tokenService.CreateToken(user)
        };
        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto, [FromQuery] bool roleAdmin = false)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = new AppUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };
            var createdUser = await _userManager.CreateAsync(appUser, userDto.Password);
            if(createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, !roleAdmin ? "User" : "Admin");
                if(roleResult.Succeeded)
                {
                    CreateUserResponseDto response = new CreateUserResponseDto
                    {
                        UserName = appUser.UserName,
                        UserEmail = appUser.Email,
                        AccessToken = _tokenService.CreateToken(appUser)
                    };
                    return Ok(response);
                        
                } else
                {
                    return BadRequest(roleResult.Errors);
                }
            } else
            {
                return BadRequest(createdUser.Errors);
            }
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
