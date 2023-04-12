using Microsoft.AspNetCore.Mvc;

namespace ImaPayAPI.Controllers
{
    public class ImaPayController : Controller
    {
        // Colocar injenção de dependência do contexto
        public ImaPayController() { }

        // Cadastro de Usuarios (Colocar rota)
        public ActionResult Register()
        {
            return Ok();
        }

        // Login (Colocar rota)
        public ActionResult Login()
        {
            return Ok();
        }


        // Informações do usuário (Colocar rota)
        public ActionResult Info()
        {
            return Ok();
        }

        // Transferência (Colocar rota)
        public ActionResult Transfer()
        {
            return Ok();
        }


        // Histórico de Transações (Colocar rota)
        public ActionResult TransferHistory()
        {
            return Ok();
        }

    
    }
}
