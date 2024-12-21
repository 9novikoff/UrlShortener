using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.DAL.Entities;

namespace UrlShortener.Api.DAL;

public class UrlsDbContext: DbContext
{
    public UrlsDbContext(DbContextOptions<UrlsDbContext> options) : base(options)
    {
        
    }

    public DbSet<Url> Urls { get; set; }
    public DbSet<User> Users { get; set; }
}