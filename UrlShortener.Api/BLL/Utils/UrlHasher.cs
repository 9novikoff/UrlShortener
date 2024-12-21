using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Api.BLL.Utils;

public static class UrlHasher
{
    public static string Get8LengthHash(string url)
    {
        using HashAlgorithm algorithm = SHA256.Create();
        var byteHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(url));
        var base64 = Convert.ToBase64String(byteHash).Replace('+', '-').Replace('/', '_');
        return base64[..8];
    }
}