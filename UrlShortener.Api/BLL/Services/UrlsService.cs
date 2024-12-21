using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.BLL.Errors;
using UrlShortener.Api.BLL.Utils;
using UrlShortener.Api.DAL.Entities;
using UrlShortener.Api.DAL.Repositories;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.BLL.Services;

class UrlsService : IUrlsService
{
    private readonly IUrlsRepository _urlsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly AbstractValidator<UrlCreateDto> _urlValidator;
    private readonly IMapper _mapper;

    public UrlsService(IUrlsRepository urlsRepository, IUsersRepository usersRepository, AbstractValidator<UrlCreateDto> urlValidator, IMapper mapper)
    {
        _urlsRepository = urlsRepository;
        _usersRepository = usersRepository;
        _urlValidator = urlValidator;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<UrlDto>, string>> GetAllUrls()
    {
        var urls = await _urlsRepository.GetUrls().ToListAsync();

        return _mapper.Map<List<UrlDto>>(urls);
    }

    public async Task<ServiceResult<UrlDto, UrlAddFailed>> AddUrl(Guid userId, UrlCreateDto url)
    {
        var validationResult = await _urlValidator.ValidateAsync(url);

        if (!validationResult.IsValid)
        {
            return new UrlAddFailed(validationResult.ToString());
        }

        if (_urlsRepository.GetUrls().Any(u => u.OriginalUrl == url.OriginalUrl))
        {
            return new UrlAddFailed("Short url for this url already exists");
        }

        var urlToInsert = _mapper.Map<Url>(url, opt => opt.AfterMap((_, dest) => 
        {
            dest.CreatedDate = DateTime.Now;
            dest.UserId = userId;
            dest.ShortUrl = UrlHasher.Get8LengthHash(dest.OriginalUrl);
        }));

        return _mapper.Map<UrlDto>(await _urlsRepository.InsertUrl(urlToInsert));
    }

    public async Task<ServiceResult<bool, UrlDeleteFailed>> DeleteUrl(Guid userId, Guid urlId)
    {
        var urlToDelete = await _urlsRepository.GetUrlById(urlId);

        if (urlToDelete == null)
        {
            return new UrlDeleteFailed("No url with such id");
        }

        var user = await _usersRepository.GetUserById(userId);

        if (user == null)
        {
            return new UrlDeleteFailed("No user with such id");
        }
        
        if (user.Role != Role.Admin && urlToDelete.UserId != userId)
        {
            return new UrlDeleteFailed("Not admin users cannot delete others urls");
        }

        await _urlsRepository.DeleteUrl(urlId);
        return true;
    }

    public async Task<ServiceResult<UrlExtendedDto, UrlGetDetailedFailed>> GetUrlById(Guid userId, Guid urlId)
    {
        var url = await _urlsRepository.GetUrlById(urlId);

        if (url == null)
        {
            return new UrlGetDetailedFailed("No url with such id");
        }
        
        var user = await _usersRepository.GetUserById(userId);

        if (user == null)
        {
            return new UrlGetDetailedFailed("No user with such id");
        }
        
        if (user.Role != Role.Admin && url.UserId != userId)
        {
            return new UrlGetDetailedFailed("Not admin users cannot get info about others urls");
        }

        return _mapper.Map<UrlExtendedDto>(url, opt => opt.AfterMap((_, dest) => dest.CreatedBy = user.Email));
    }
}