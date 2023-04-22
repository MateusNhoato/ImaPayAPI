using ImaPayAPI.Context;
using ImaPayAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ImaPayAPI.Services.Token
{
    public class ValidateAndReturnUserService
    {
        private ImayPayContext _context;

        public ValidateAndReturnUserService(ImayPayContext context)
        {
            _context = context;
        }

        public User Validate(string token)
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(TokenSettings.Secret);

            //tokenHandler.ValidateToken(token, new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(key),
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    ClockSkew = TimeSpan.Zero
            //}, out SecurityToken validatedToken);

            //var jwtToken = (JwtSecurityToken)validatedToken;
            //var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

            //return int.Parse(userId);

            User? user = _context.userToken[token];

            if (user == null)
                throw new UnauthorizedAccessException("Usuário não autorizado.");

            return user;
        }
    }
}
