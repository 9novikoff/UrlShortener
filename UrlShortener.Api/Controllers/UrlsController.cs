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
    public async Task<IActionResult> GetDetailedUrl(Guid id)
    {
        var serviceResult = await _service.GetUrlById(GetUserIdFromJwtToken(), id);
        return serviceResult.Match<IActionResult>(s => Ok(s), f => NotFound(f.ErrorMessage));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddUrl(UrlCreateDto urlCreateDto)
    {
        var urlBase = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
        
        var serviceResult = await _service.AddUrl(GetUserIdFromJwtToken(), urlCreateDto, urlBase);

        return serviceResult.Match<IActionResult>(s => Ok(s), f => NotFound(f.ErrorMessage));
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUrl(Guid id)
    {
        var serviceResult = await _service.DeleteUrl(GetUserIdFromJwtToken(), id);

        return serviceResult.Match<IActionResult>(s => Ok(), f => NotFound(f.ErrorMessage));
    }
    
    private Guid GetUserIdFromJwtToken()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        return Guid.Parse(userIdClaim.Value);
    }
}