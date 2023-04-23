using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Services.Token;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuth.Services
{
    public class TokenService
    {
        private ImayPayContext _context;

        public TokenService(ImayPayContext context) 
        {
            _context = context;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName), // User.Identity.Name
                    new Claim(ClaimTypes.Role, user.Role), // User.IsRole()
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User? Validate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSettings.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userId = int.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value);

            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}