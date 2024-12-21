namespace UrlShortener.Api.BLL.Errors;

public class UserCreationFailed : ErrorBase
{
    public UserCreationFailed(string errorMessage) : base(errorMessage)
    {
    }
}