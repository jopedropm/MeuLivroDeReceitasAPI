using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;
public class PasswordEncripter
{
    public string Encrypt(string password)
    {
        //Adicionando algo a mais na senha que somente nossa API conhece
        var additionalKey = "ABC";
        var newPassword = $"{password}{additionalKey}";

        //Criptografando
        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
