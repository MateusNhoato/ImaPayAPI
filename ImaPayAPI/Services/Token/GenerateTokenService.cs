using ImaPayAPI.Models;
using ImaPayAPI.Services.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImaPayAPI.Services
{
    public class GenerateTokenService
    {
        private LinkUserToTokenService linkUserToTokenService;

        public GenerateTokenService(LinkUserToTokenService linkUserToTokenService)
        {
            this.linkUserToTokenService = linkUserToTokenService;
        }

        public string Gerar(User user, DateTime? Expires)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Expires = Expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);


            linkUserToTokenService.Link(encodedToken, user);

            return encodedToken;
        }
    }
}
