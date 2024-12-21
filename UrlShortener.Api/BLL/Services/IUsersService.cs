using UrlShortener.Api.BLL.Errors;
using UrlShortener.Api.BLL.Validators;
using UrlShortener.Api.DAL.Entities;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.BLL.Services;

public interface IUsersService
{
    public Task<ServiceResult<UserDto, UserCreationFailed>> CreateUser(RegisterUserDto registerUser, Role roles = 0);
    public Task<ServiceResult<string, LoginUserFailed>> LoginUser(LoginUserDto loginUserDto);
}