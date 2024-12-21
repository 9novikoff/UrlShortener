using UrlShortener.Api.DAL.Entities;

namespace UrlShortener.Api.DAL.Repositories;

public interface IUsersRepository
{
    public IQueryable<User> GetUsers();
    public Task<User> InsertUser(User user);
    public Task<User> UpdateUser(User user);
    public Task DeleteUser(Guid id);
    public Task<User?> GetUserById(Guid id);
}