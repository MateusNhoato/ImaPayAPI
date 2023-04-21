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
    [Route("api/[controller]")]
    [ApiController]
    public class ImaPayController : Controller
    {
        private ImayPayContext _context;
        private IMapper _mapper;

        public ImaPayController(ImayPayContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

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
        
        protected int? ValidateUserAndGetId(string token)
        {   
            try
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
                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

                return int.Parse(userId);
            }
            catch
            {
                return null;
            }
        }

        // Registro do usuário
        [HttpPost("api/[controller]/Register")]
        public ActionResult<UserInfoDTO> Register(UserRegisterDTO userDto)
        {
            try
            {
                if (userDto is null)
                    return BadRequest(new
                    {
                        Moment = DateTime.Now,
                        Message = "Não foi possível cadastrar o usuário."
                    });


                User user = _mapper.Map<User>(userDto);
                user.Balance = 5000;
                user.Agency = 0001;
                user.Account = Guid.NewGuid().ToString();


                _context.Users.Add(user);
                _context.SaveChanges();

                var userToAdd = _mapper.Map<UserInfoDTO>(user);

                if (userToAdd == null) return BadRequest(new
                {
                    Moment = DateTime.Now,
                    Message = $"Não foi possível cadastrar o usuário."
                });

                return Ok(userToAdd);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Moment = DateTime.Now,
                    Message = "Não foi possível cadastrar o usuário."
                });
            }
        }

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
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Account == account);

                if (user == null) return NotFound(new
                {
                    Moment = DateTime.Now,
                    Message = $"Usuário da conta {account} não encontrado."
                });

                var userAccount = _mapper.Map<UserInfoDTO>(user);
                return Ok(userAccount);
            }
            catch (Exception)
            {

                return StatusCode(500, new
                {
                    Moment = DateTime.Now,
                    Message = "Não foi possível encontrar o usuário."
                });
            }

        }


        // Transferência
        [HttpPost("Transfer")]
        public ActionResult Transfer([FromBody] TransactionDTO transactionDTO)
        {
            try
            {
                var valueTransfer = transactionDTO.ValueTransaction;
                var user = _context.Users.Find(transactionDTO.UserId);
                decimal balance = (decimal)_context.Entry(user).Property(u => u.Balance).CurrentValue;
                

                if (valueTransfer > balance)
                {

                    return NotFound(new { Message = $"Sem saldo: {balance}, valor da transferência {valueTransfer}" });
                }
                else
                {
                    user.Balance = balance - valueTransfer;
                }

                var transaction = _mapper.Map<Transaction>(transactionDTO);
               // string date = String.Format("{yyyy-MM-dd", transaction.Date);
                if (transaction.Date.Day != DateTime.Today.Day)
                    transaction.Status = "Agendada";
                else
                    transaction.Status = "Realizada";
                
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return Ok(_mapper.Map<TransactionInfoDTO>(transaction));
            }
            catch(Exception) 
            {
                return StatusCode(500, new
                {
                    Moment = DateTime.Now,
                    Message = "Não foi possível concluir a transação. Tente novamente mais tarde."
                });
            }
            

        }

        // Histórico de Transações
        [HttpGet("TransferHistory")]
        public ActionResult TransferHistory(int userId)
        {
            try
            {
                List<Transaction> transactions = _context.Transactions
                    .Where(t => t.Id == userId)
                    .OrderByDescending(t => t.Date)
                    .ToList();

                // Retorna as transações em um objeto TransferHistoryDTO
                TransferHistoryDTO transferHistory = new TransferHistoryDTO
                {
                    Transactions = transactions
                };

                return Ok(transferHistory);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
