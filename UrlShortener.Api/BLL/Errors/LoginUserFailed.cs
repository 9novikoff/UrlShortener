namespace UrlShortener.Api.BLL.Errors;

public class LoginUserFailed: ErrorBase
{
    public LoginUserFailed(string errorMessage) : base(errorMessage)
    {
    }
}