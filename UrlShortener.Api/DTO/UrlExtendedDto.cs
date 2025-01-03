﻿namespace UrlShortener.Api.DTO;

public class UrlExtendedDto
{
    public Guid Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortUrl { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}