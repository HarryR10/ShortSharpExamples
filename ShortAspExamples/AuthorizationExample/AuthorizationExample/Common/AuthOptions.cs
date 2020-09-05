using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Common
{
    public class AuthOptions
    {
        //см appsettings.json
        public string Issuer { get; set; } //сервер аутентификации
        public string Audience { get; set; } //ресурс (сервер) для кого токен предназначается
        public string Secret { get; set; } //строка для генерации ключа шифрования
        public int TokenLifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecureityKey =>
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

    }
}
 