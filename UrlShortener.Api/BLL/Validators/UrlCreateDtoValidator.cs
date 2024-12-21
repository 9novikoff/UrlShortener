using System.Text.RegularExpressions;
using FluentValidation;
using UrlShortener.Api.DTO;

namespace UrlShortener.Api.BLL.Validators;

public class UrlCreateDtoValidator: AbstractValidator<UrlCreateDto>
{
    public UrlCreateDtoValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty()
            .Must(BeAValidUrl).WithMessage("Invalid URL format.");
    }
    
    private bool BeAValidUrl(string url)
    {
        var regex = new Regex(@"^(http(s)?://)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(/.*)?$");

        return regex.IsMatch(url);
    }
}