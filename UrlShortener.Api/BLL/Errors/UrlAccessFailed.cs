namespace UrlShortener.Api.BLL.Errors;

public class UrlAccessFailed : ErrorBase
{
    public UrlAccessFailed(string errorMessage) : base(errorMessage)
    {
    }
}