using ApiAuth.Services;
using AutoMapper;
using ImaPayAPI.Context;
using ImaPayAPI.Services.DTO;

namespace ImaPayAPI.Services
{
    public class BaseService
    {
        protected ImayPayContext _context;
        protected IMapper _mapper;
        protected DtoService _dtoService;
        protected TokenService _tokenService;

        public BaseService(ImayPayContext context, IMapper mapper, DtoService dtoService, TokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _dtoService = dtoService;
            _tokenService = tokenService;
        }
    }
}
