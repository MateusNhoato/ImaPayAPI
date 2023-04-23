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
using ApiAuth.Services;

namespace ImaPayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImaPayController : Controller
    {
        private RegisterUserService _registerUserService;
        private LoginService _loginService;
        private TransferService _transferService;
        private TransferHistoryService _transferHistoryService;

        private DtoService _dtoService;
        private TokenService _tokenService;

        public ImaPayController(RegisterUserService registerUserService,
                                LoginService loginService,
                                DtoService dtoService,
                                TransferService transferService,
                                TransferHistoryService transferHistoryService,
                                TokenService tokenService
                                )
        {
            _registerUserService = registerUserService;
            _loginService = loginService;
            _dtoService = dtoService;
            _transferService = transferService;
            _transferHistoryService = transferHistoryService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public ActionResult Register(UserRegisterDTO userDto)
        {
            _registerUserService.Register(userDto);
            return Ok("Usuário cadastrado com sucesso!");

        }

        [HttpPost("Login")]
        public ActionResult<UserLoginDTO> Login(UserLoginDTO dto)
        {
            var token = _loginService.Login(dto);
            return Ok(token);
        }

        [HttpGet("Info")]
        public ActionResult<UserInfoDTO> Info([FromHeader] string token)
        {
            var user = _tokenService.Validate(token);
            var userDto = _dtoService.GetUserInfoDTO(user);
            return Ok(userDto);
        }

        [HttpPost("Transfer")]
        public ActionResult Transfer([FromBody] TransactionDTO transactionDTO,
                                     [FromHeader] string token)
        {
            var user = _tokenService.Validate(token);
            var transaction = _transferService.Transfer(transactionDTO, user);

            var transactionInfoDTO = _dtoService.GetTransactionInfoDTO(transaction);
            
            return Ok(transactionInfoDTO);
        }

        [HttpGet("TransferHistory")]
        public ActionResult TransferHistory([FromHeader] string token)
        {
            var transactions = _transferHistoryService.GetTransferHistory(token);
            return Ok(transactions);
        }
    }
}
