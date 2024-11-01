using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Services
{
    public class TokenGenerator
    {
        private readonly TokenOptions _tokenOptions;
        public TokenGenerator()
        {
           _tokenOptions = new TokenOptions {
                Secret = "4d82a63bbdc67c1e4784ed6587f3730c",
                ExpiresDay = 1
           };

        }
        
        public string Generate(UserDto user)
        {
            if (string.IsNullOrEmpty(_tokenOptions.Secret))
            {
                throw new ArgumentNullException(nameof(_tokenOptions.Secret), "O segredo do token não pode ser nulo ou vazio.");
            }      
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = AddClaims(user),
                Expires = System.DateTime.UtcNow.AddDays(_tokenOptions.ExpiresDay),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenOptions.Secret)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);  
        }

        private ClaimsIdentity AddClaims(UserDto user)
        {
            var Claims = new ClaimsIdentity();
            Claims.AddClaim(new Claim(ClaimTypes.Name, user.Name!));
            Claims.AddClaim(new Claim(ClaimTypes.Email, user.Email!));
            if (user.UserType == "admin")
            {
                Claims.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            }
            return Claims;
        }
    }
}