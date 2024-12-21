using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using UrlShortener.Api.BLL.Services;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("urls")]
public class UrlsController: ControllerBase
{
    private readonly IUrlsService _service;

    public UrlsController(IUrlsService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUrls()
    {
        var serviceResult = await _service.GetAllUrls();

        return serviceResult.Match<IActionResult>(s => Ok(s), f => NotFound(f));
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetailedUrl([FromQuery] Guid id)
    {
        var serviceResult = await _service.GetUrlById(GetUserIdFromJwtToken(), id);
        return serviceResult.Match<IActionResult>(s => Ok(s), f => NotFound(f.ErrorMessage));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddUrl(UrlCreateDto urlCreateDto)
    {
        var serviceResult = await _service.AddUrl(GetUserIdFromJwtToken(), urlCreateDto);

        return serviceResult.Match<IActionResult>(s => Ok(s), f => NotFound(f.ErrorMessage));
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteUrl([FromQuery]Guid urlId)
    {
        var serviceResult = await _service.DeleteUrl(GetUserIdFromJwtToken(), urlId);

        return serviceResult.Match<IActionResult>(s => Ok(), f => NotFound(f.ErrorMessage));
    }
    
    private Guid GetUserIdFromJwtToken()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        return Guid.Parse(userIdClaim.Value);
    }
}