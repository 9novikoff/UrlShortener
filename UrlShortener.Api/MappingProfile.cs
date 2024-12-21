using AutoMapper;
using UrlShortener.Api.DAL.Entities;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        
        CreateMap<RegisterUserDto, User>();
        CreateMap<User, UserDto>();
        CreateMap<Url, UrlDto>();
        CreateMap<Url, UrlExtendedDto>();
    }
    
}