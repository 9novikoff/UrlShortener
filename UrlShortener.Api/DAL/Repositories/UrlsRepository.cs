using UrlShortener.Api.DAL.Entities;

namespace UrlShortener.Api.DAL.Repositories;

public class UrlsRepository : IUrlsRepository
{
    private UrlsDbContext _dbContext;

    public UrlsRepository(UrlsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Url> GetUrls()
    {
        return _dbContext.Urls;
    }

    public async Task<Url> InsertUrl(Url url)
    {
        _dbContext.Add(url);
        await _dbContext.SaveChangesAsync();
        return url;
    }

    public async Task<Url> UpdateUrl(Url url)
    {
        _dbContext.Update(url);
        await _dbContext.SaveChangesAsync();
        return url;
    }

    public async Task DeleteUrl(Guid id)
    {
        var url = await _dbContext.FindAsync<Url>(id);
        _dbContext.Remove(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Url?> GetUrlById(Guid id)
    {
        return await _dbContext.FindAsync<Url>(id);
    }
}