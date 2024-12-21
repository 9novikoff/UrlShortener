namespace UrlShortener.Api.BLL.Errors;

public class UrlGetDetailedFailed : ErrorBase
{
    public UrlGetDetailedFailed(string errorMessage) : base(errorMessage)
    {
    }
}