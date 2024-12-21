using UrlShortener.Api.BLL.Errors;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.BLL.Services;

public interface IUrlsService
{
    public Task<ServiceResult<List<UrlDto>, string>> GetAllUrls();
    public Task<ServiceResult<UrlDto, UrlAddFailed>> AddUrl(Guid userId, UrlCreateDto url);
    public Task<ServiceResult<bool, UrlDeleteFailed>> DeleteUrl(Guid userId, Guid urlId);
    public Task<ServiceResult<UrlExtendedDto, UrlGetDetailedFailed>> GetUrlById(Guid userId, Guid urlId);
}