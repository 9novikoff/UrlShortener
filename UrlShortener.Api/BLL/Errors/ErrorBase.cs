namespace UrlShortener.Api.BLL.Errors;

public abstract class ErrorBase
{
    public string ErrorMessage { get; }

    protected ErrorBase(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}