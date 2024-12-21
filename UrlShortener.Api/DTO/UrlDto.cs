namespace UrlShortener.Api.DTO;

public class UrlDto
{
    public Guid Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
    public Guid UserId { get; set; }
}