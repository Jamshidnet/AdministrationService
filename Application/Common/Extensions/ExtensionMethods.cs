using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Extensions;

public static class ExtensionMethods
{
    public static string GetHashedString(this string text)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hashbytes = sha256.ComputeHash(bytes);
            text = Convert.ToBase64String(hashbytes);
        }

        return text;
    }
}
