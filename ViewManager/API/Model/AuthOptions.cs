using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Model
{
    public class AuthOptions
    {
        public const string ISSUER = "Api"; // издатель токена
        public const string AUDIENCE = "Server"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 40; // время жизни токена - 40 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
