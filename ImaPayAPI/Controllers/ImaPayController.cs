using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImaPayAPI.Controllers
{
    [ApiController]
    public class ImaPayController : Controller
    {
        private ImayPayContext _context;
        private IMapper _mapper;

        public ImaPayController(ImayPayContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        // Gerar Token
        protected string GerarJwt(User user, DateTime? Expires)
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

            return encodedToken;
        }      
                              
        // Registro do usuário
        [HttpPost("api/[controller]/Register")]
        public ActionResult<UserInfoDTO> Register(UserRegisterDTO user)
        {
            /*var result = _context.Users.Register(user);

            if (result == null) return BadRequest(new {
                Moment = DateTime.Now,
                Message = $"Não foi possível cadastrar o usuário."
            });
            return Ok(result);*/
            return Ok();
        }             

        // Login 
        [HttpPost("Login")]
        public ActionResult<UserLoginDTO> Login(string email, string password)
        {
            try
            {
                // Encontrar usuário pelo email
                var user = _context.Users.FirstOrDefault(x => x.Email == email);

                // Validação email
                if (user == null) return NotFound(new
                {
                    Moment = DateTime.Now,
                    Message = $"Email não encontrado."
                });

                // Validação senha
                if (user.Password != password) return NotFound(new
                {
                    Moment = DateTime.Now,
                    Message = $"Senha incorreta."
                });

                var userAccount = _mapper.Map<UserLoginDTO>(user);

                // Chamando método para gerar token ao logar
                var Expires = DateTime.Now.AddHours(2);
                GerarJwt(user, Expires);

                return Ok(userAccount);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Moment = DateTime.Now,
                    Message = "Não foi possível acessar o servidor. Tente mais tarde."
                });
            }
        }

        [HttpGet("api/[controller]/Info")]

        // Informações do usuário 
        public ActionResult<UserInfoDTO> Info(string account)
        {
            var user = _context.Users.FirstOrDefault(u => u.Account == account);

            if (user == null) return NotFound(new {
                Moment = DateTime.Now,
                Message = $"Usuário da conta {account} não encontrado."
            });

            var userAccount = _mapper.Map<UserInfoDTO>(user);
            return Ok(userAccount);

        }


        // Transferência
        [HttpPatch("api/[controller]/Transfer")]
        public ActionResult Transfer()
        {
            return Ok();
        }

        [HttpGet("api/[controller]/TransferHistory")]

        // Histórico de Transações 
        public ActionResult TransferHistory()
        {
            return Ok();
        }

    
    }
}
