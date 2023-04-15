using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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

        // Registro do usuário
        [HttpPost("api/[controller]/Register")]
        public  ActionResult Register()
        {       

            return Ok();
        }

        // Login 
        [HttpGet("api/[controller]/Login")]
        public ActionResult Login()
        {
            
            return Ok();
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
