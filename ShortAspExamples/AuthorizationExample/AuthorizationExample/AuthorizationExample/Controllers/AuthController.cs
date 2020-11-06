using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthorizationExample.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationExample.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthController(IOptions<AuthOptions> options)
        {
            _authOptions = options;
        }

        private List<Account> Accounts => new List<Account>
        {
            new Account() {
                Id = Guid.Parse("e2371dc9-a849-4f3c-9004-df8fc921c13a"),
                Email = "user@mail.com",
                Password = "user",
                Roles = new Role[] {Role.User}
            },
            new Account() {
                Id = Guid.Parse("7b0a1ec1-a849-4f3c-9004-df8fc921c13a"),
                Email = "user2@mail.com",
                Password = "user2",
                Roles = new Role[] {Role.User}
            },
            new Account() {
                Id = Guid.Parse("8e7eb047-a849-4f3c-9004-df8fc921c13a"),
                Email = "admin@mail.com",
                Password = "admin",
                Roles = new Role[] {Role.Admin}
            }
        };

        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody]Login request)
        {
            //атрибут FromBody используется для привязки параметров
            //к значениям, переданным в теле POST-запроса,  
            //https://metanit.com/sharp/aspnet_webapi/2.6.php

            var user = AuthentificateUser(request.Email, request.Password);

            if (user != null)
            {
                return Ok(
                    new
                    {
                        access_token = GenerateJWT(user)
                    });
            }

            return Unauthorized();
        }

        private Account AuthentificateUser(string email, string password) =>
            Accounts.SingleOrDefault(u => u.Email == email
                && u.Password == password);

        private string GenerateJWT(Account user)
        {
            var authParams = _authOptions.Value;
            var securityKey = authParams.GetSymmetricSecureityKey;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() //эти Claims - стандартные
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            //роли в Claims добавляем вручную, но как "role", т.к. маппинг
            foreach (var role in user.Roles) 
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
