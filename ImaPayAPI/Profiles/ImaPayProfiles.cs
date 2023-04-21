using AutoMapper;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;

namespace ImaPayAPI.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionDTO, Transaction>();
            CreateMap<Transaction, TransactionInfoDTO>();
            CreateMap<TransactionInfoDTO, Transaction>();
        }
    }
}
