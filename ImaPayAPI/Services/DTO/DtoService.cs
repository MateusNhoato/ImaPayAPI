using AutoMapper;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;

namespace ImaPayAPI.Services.DTO
{
    public class DtoService
    {
        private IMapper _mapper;

        public DtoService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserInfoDTO GetUserInfoDTO(User user)
        {
            UserInfoDTO userDTO = _mapper.Map<UserInfoDTO>(user);

            return userDTO;
        }

        public Transaction GetTransactionFromTransactionDTO(TransactionDTO transactionDto) 
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            return transaction;
        }

        public TransferHistoryDTO GetTransactionHistoryDTO(List<Transaction> transactions) 
        {
            var transactionHistoryDto = new TransferHistoryDTO
            {
                Transactions = transactions
            };   

            return transactionHistoryDto;
        }

        public TransactionInfoDTO GetTransactionInfoDTO(Transaction transaction)
        {
            var transactionDTO = _mapper.Map<TransactionInfoDTO>(transaction);

            return transactionDTO;
        }

    }
}
