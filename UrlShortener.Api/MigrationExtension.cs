using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.DAL;

namespace UrlShortener.Api;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<UrlsDbContext>();
        context.Database.Migrate();
    }
}