using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ImaPayAPI.Controllers
{
    [ApiController]
    public class ImaPayController : Controller
    {
        private ImayPayContext _context;
       
        public ImaPayController(ImayPayContext context) {
            _context = context;
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
        public ActionResult Info()
        {
            return Ok();
        }


        // Transferência
        [HttpPatch("api/[controller]/Transfer")]
        public ActionResult Transfer()
        {
            return Ok();
        }

        [HttpGet("api/[controller]/Hist")]

        // Histórico de Transações 
        public ActionResult TransferHistory()
        {
            return Ok();
        }

    
    }
}
