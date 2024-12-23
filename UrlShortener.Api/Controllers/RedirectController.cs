using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.BLL.Services;

namespace UrlShortener.Api.Controllers;

public class RedirectController : ControllerBase
{
    private readonly IUrlsService _urlsService;
    private readonly IConfiguration _configuration;

    public RedirectController(IUrlsService urlsService, IConfiguration configuration)
    {
        _urlsService = urlsService;
        _configuration = configuration;
    }

    [HttpGet("{url}")]
    public async Task<IActionResult> AccessShortUrl(string url)
    {
        var urlBase = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
        
        var serviceResult = await _urlsService.GetUrlByShortUrl(urlBase + url);

        return serviceResult.Match<IActionResult>(s => Redirect(s),
            f => Redirect(_configuration["ErrorUrl:RedirectUrl"]));
    }
}