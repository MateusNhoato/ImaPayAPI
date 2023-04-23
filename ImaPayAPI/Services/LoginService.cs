using ApiAuth.Services;
using AutoMapper;
using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Services.DTO;
using ImaPayAPI.Services.Exceptions;

namespace ImaPayAPI.Services
{
    public class LoginService : BaseService
    {
        public LoginService(ImayPayContext context, 
                            IMapper mapper, 
                            DtoService dtoService,
                            TokenService tokenService) 
                            : base(context, mapper, dtoService, tokenService){}

        public string Login(UserLoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);

            if (user == null || user.Password != dto.Password)
                throw new NotFoundException($"Email e/ou senha inválidos.");

            var token = _tokenService.GenerateToken(user);
            
            return token;
        }
    }
}
