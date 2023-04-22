using AutoMapper;
using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection.Metadata.Ecma335;

namespace ImaPayAPI.Services
{
    public class RegisterUserService
    {
        private ImayPayContext _context;
        private IMapper _mapper;

        public RegisterUserService(ImayPayContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User Register(UserRegisterDTO userDto)
        {
            if (userDto is null)
                throw new BadHttpRequestException("Informações do usuário inválidas.");

            User user = _mapper.Map<User>(userDto);
            user.Balance = new Random().Next(0, 50000);
            user.Agency = 0001;
            user.Savings = new Random().Next(0, 50000);
            user.Investments = new Random().Next(0, 50000);
            user.Account = "24124124";

            if(user == null)
                throw new BadHttpRequestException("Informações do usuário inválidas.");
            foreach (User u in _context.Users) 
            {
                if (user.Cpf == u.Cpf || user.Email == u.Email)
                    throw new NotFoundException("Usuário já cadastrado.");
            }

             _context.Users.Add(user);
             _context.SaveChanges();

            return user;
        }
    }
}
