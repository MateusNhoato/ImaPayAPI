using AutoMapper;
using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Services.Exceptions;

namespace ImaPayAPI.Services
{
    public class LoginService
    {
        private ImayPayContext _context;
        private IMapper _mapper;
        private GenerateTokenService _generateTokenService;

        public LoginService(ImayPayContext context, IMapper mapper,GenerateTokenService generateTokenService )
        {
            _context = context;
            _mapper = mapper;
            _generateTokenService = generateTokenService;
        }

        public string Login(UserLoginDTO dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);

            if (user == null || user.Password != dto.Password)
                throw new NotFoundException($"Email e/ou senha inválidos.");

            var Expires = DateTime.Now.AddHours(2);
            var token = _generateTokenService.Gerar(user, Expires);

            return token;
        }
    }
}
