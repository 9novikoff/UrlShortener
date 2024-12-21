namespace UrlShortener.Api.BLL.Errors;

public class UrlDeleteFailed: ErrorBase
{
    public UrlDeleteFailed(string errorMessage) : base(errorMessage)
    {
    }
}