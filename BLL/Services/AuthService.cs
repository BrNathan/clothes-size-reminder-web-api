using BLL.Interfaces;
using Entities.ExtendedModels;
using Entities.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthService(
            IOptions<AppSettings> appSettings,
            IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        public UserAuthenticate Authenticate(string email, string passwordHash)
        {
            User user = _userService.GetUserByEmailPaswword(email, passwordHash);

            if (user == null)
            {
                return null;
            }

            UserAuthenticate userAuthenticate = new UserAuthenticate(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userAuthenticate.Token = tokenHandler.WriteToken(token);

            return userAuthenticate;
        }
    }
}
