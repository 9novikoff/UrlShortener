using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.BLL.Services;
using UrlShortener.Api.DAL.Entities;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.Controllers;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;

    public UsersController(IUsersService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserDto registerDto)
    {
        var result = await _service.CreateUser(registerDto);
        return result.Match<IActionResult>(u => Ok(), f => StatusCode(403, f.ErrorMessage));
    }
    
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin(RegisterUserDto registerDto)
    {
        var result = await _service.CreateUser(registerDto, Role.Admin);
        return result.Match<IActionResult>(u => Ok(), f => StatusCode(403, f.ErrorMessage));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserDto loginDto)
    {
        var result = await _service.LoginUser(loginDto);
        return result.Match<IActionResult>(token => Ok(token), f => StatusCode(403, f.ErrorMessage));
    }
}