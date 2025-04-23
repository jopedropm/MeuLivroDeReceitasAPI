using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptography;
public class PasswordEncripter
{
    private readonly string _additionalKey;
    public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;

    public string Encrypt(string password)
    {
        //Adicionando algo a mais na senha que somente nossa API conhece
        var newPassword = $"{password}{_additionalKey}";

        //Criptografando
        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
