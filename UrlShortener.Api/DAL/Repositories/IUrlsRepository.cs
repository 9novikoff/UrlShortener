using UrlShortener.Api.DAL.Entities;

namespace UrlShortener.Api.DAL.Repositories;

public interface IUrlsRepository
{
    public IQueryable<Url> GetUrls();
    public Task<Url> InsertUrl(Url url);
    public Task<Url> UpdateUrl(Url url);
    public Task DeleteUrl(Guid id);
    public Task<Url?> GetUrlById(Guid id);
}