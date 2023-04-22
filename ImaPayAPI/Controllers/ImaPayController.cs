using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImaPayAPI.Services;
using ImaPayAPI.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using ImaPayAPI.Services.Token;
using ImaPayAPI.Services.DTO;

namespace ImaPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImaPayController : Controller
    {
        private RegisterUserService _registerUserService;
        private LoginService _loginService;
        private ValidateAndReturnUserService _validateAndReturnUserService;
        private TransferService _transferService;
        private TransferHistoryService _transferHistoryService;
        
        private DtoService _dtoService;



        public ImaPayController(RegisterUserService registerUserService, 
                                LoginService loginService, 
                                ValidateAndReturnUserService validateAndReturnUserService,
                                DtoService dtoService,
                                TransferService transferService,
                                TransferHistoryService transferHistoryService
                                )
        {
            _registerUserService = registerUserService;
            _loginService = loginService;
            _validateAndReturnUserService = validateAndReturnUserService;
            _dtoService = dtoService;
            _transferService = transferService;
            _transferHistoryService = transferHistoryService;
            
        }


        // Registro do usuário
        [HttpPost("api/[controller]/Register")]
        public ActionResult Register(UserRegisterDTO userDto)
        {
            try
            {
                _registerUserService.Register(userDto);
                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception e)
            {
                switch (e){
                    case BadHttpRequestException:
                        return BadRequest(e.Message);
                    case NotFoundException:
                        return NotFound(e.Message);
                    case UnauthorizedAccessException: 
                        return Unauthorized(e.Message);
                    default: 
                        return StatusCode(500, "Houve algum problema no servidor.");
                }
            }
        }

        [HttpPost("Login")]
        public ActionResult<UserLoginDTO> Login(UserLoginDTO dto)
        {
            try
            {
                var token = _loginService.Login(dto);

                return Ok(token);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case BadHttpRequestException:
                        return BadRequest(e.Message);
                    case NotFoundException:
                        return NotFound(e.Message);
                    case UnauthorizedAccessException:
                        return Unauthorized(e.Message);
                    default:
                        return StatusCode(500, "Houve algum problema no servidor.");
                }
            }
        }

        [HttpGet("api/[controller]/Info")]
        // Informações do usuário 
        public ActionResult<UserInfoDTO> Info([FromHeader] string token)
        {
            try
            {
                 var user = _validateAndReturnUserService.Validate(token);
                 
                 var userDto = _dtoService.GetUserInfoDTO(user);

                return Ok(userDto);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case BadHttpRequestException:
                        return BadRequest(e.Message);
                    case NotFoundException:
                        return NotFound(e.Message);
                    case UnauthorizedAccessException:
                        return Unauthorized(e.Message);
                    default:
                        return StatusCode(500, "Houve algum problema no servidor.");
                }
            }

        }


        // Transferência
        [HttpPost("Transfer")]
        public ActionResult Transfer([FromBody] TransactionDTO transactionDTO, [FromHeader] string token)
        {
            try
            {
               var user =_validateAndReturnUserService.Validate(token);
               var transaction = _transferService.Transfer(transactionDTO, user);
                
                return Ok(transaction);
            }
            catch(Exception e) 
            {
                switch (e)
                {
                    case BadHttpRequestException:
                        return BadRequest(e.Message);
                    case NotFoundException:
                        return NotFound(e.Message);
                    case UnauthorizedAccessException:
                        return Unauthorized(e.Message);
                    default:
                        return StatusCode(500, "Houve algum problema no servidor.");
                }
            }
            

        }

        // Histórico de Transações
        [HttpGet("TransferHistory")]
        public ActionResult TransferHistory([FromHeader] string token)
        {
            try
            {
                var transactions = _transferHistoryService.GetTransferHistory(token);

                return Ok(transactions);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case BadHttpRequestException:
                        return BadRequest(e.Message);
                    case NotFoundException:
                        return NotFound(e.Message);
                    case UnauthorizedAccessException:
                        return Unauthorized(e.Message);
                    default:
                        return StatusCode(500, "Houve algum problema no servidor.");
                }
            }
        }


    }
}
