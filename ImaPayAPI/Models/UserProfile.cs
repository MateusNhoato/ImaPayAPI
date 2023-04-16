using AutoMapper;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;

namespace Models.Profiles
{
    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<UserInfoDTO, User>();
            CreateMap<TransactionDTO, User>();
            CreateMap<UserLoginDTO, User>();
        }
    }
}