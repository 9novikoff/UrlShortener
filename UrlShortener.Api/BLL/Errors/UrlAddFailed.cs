namespace UrlShortener.Api.BLL.Errors;

public class UrlAddFailed: ErrorBase
{
    public UrlAddFailed(string errorMessage) : base(errorMessage)
    {
    }
}