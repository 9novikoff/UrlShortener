using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.DAL.Entities;

namespace UrlShortener.Api.DAL.Repositories;

class UsersRepository : IUsersRepository
{
    private readonly UrlsDbContext _dbContext;

    public UsersRepository(UrlsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<User> GetUsers()
    {
        return _dbContext.Users;
    }

    public async Task<User> InsertUser(User user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _dbContext.FindAsync<User>(id);
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _dbContext.FindAsync<User>(id);
    }
}