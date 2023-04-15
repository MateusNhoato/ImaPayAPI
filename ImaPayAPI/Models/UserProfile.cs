using AutoMapper;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;

namespace Domain.Profiles
{
    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<UserInfoDTO, User>();
        }
    }
}