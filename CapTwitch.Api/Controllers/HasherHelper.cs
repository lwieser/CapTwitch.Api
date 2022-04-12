using System.Security.Cryptography;
using System.Text;

namespace CapTwitch.Api.Controllers;

public static class HasherHelper
{
    public static string Hash(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            throw new ArgumentNullException(nameof(text));
        using (SHA256 shA256 = SHA256.Create())
        {
            byte[] hash = shA256.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte num in hash)
                stringBuilder.Append(num.ToString("x2"));
            return stringBuilder.ToString();
        }
    }
}