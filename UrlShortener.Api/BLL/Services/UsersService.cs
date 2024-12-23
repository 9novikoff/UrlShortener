using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.BLL.Errors;
using UrlShortener.Api.BLL.Utils;
using UrlShortener.Api.DAL.Entities;
using UrlShortener.Api.DAL.Repositories;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.BLL.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly AbstractValidator<RegisterUserDto> _registerValidator;
    private readonly AbstractValidator<LoginUserDto> _loginValidator;
    private readonly IMapper _mapper;
    private readonly JwtGenerator _jwtGenerator;

    public UsersService(IUsersRepository usersRepository, AbstractValidator<RegisterUserDto> registerValidator, IMapper mapper, AbstractValidator<LoginUserDto> loginValidator, JwtGenerator jwtGenerator)
    {
        _usersRepository = usersRepository;
        _registerValidator = registerValidator;
        _mapper = mapper;
        _loginValidator = loginValidator;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<ServiceResult<UserDto, UserCreationFailed>> CreateUser(RegisterUserDto registerUser, Role role = Role.User)
    {
        var validationResult = await _registerValidator.ValidateAsync(registerUser);

        if (!validationResult.IsValid)
        {
            return new UserCreationFailed(validationResult.ToString());
        }

        if (_usersRepository.GetUsers().Any(x => x.Email == registerUser.Email))
            return new UserCreationFailed("User with this email already exists");

        var user = _mapper.Map<User>(registerUser,
            opt => opt.AfterMap((_, dest) =>
            {
                dest.PasswordHash = BcryptPasswordHasher.HashPassword(registerUser.Password);
                dest.Role = role;
            }));

        var userDto = _mapper.Map<UserDto>(await _usersRepository.InsertUser(user));

        return userDto;
    }
    
    public async Task<ServiceResult<string, LoginUserFailed>> LoginUser(LoginUserDto loginUserDto)
    {
        var validationResult = await _loginValidator.ValidateAsync(loginUserDto);
        
        if (!validationResult.IsValid)
        {
            return new LoginUserFailed(validationResult.ToString());
        }

        var user = await _usersRepository.GetUsers()
            .SingleOrDefaultAsync(u => u.Email == loginUserDto.Email);

        if (user == null)
        {
            return new LoginUserFailed("No user with that email");
        }

        if (!BcryptPasswordHasher.VerifyPassword(loginUserDto.Password, user.PasswordHash))
        {
            return new LoginUserFailed("Invalid password");
        }
        
        return _jwtGenerator.GenerateToken(user);
    }
    
} 