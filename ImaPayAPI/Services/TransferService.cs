using ApiAuth.Services;
using AutoMapper;
using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Services.DTO;
using ImaPayAPI.Services.Token;

namespace ImaPayAPI.Services
{
    public class TransferService : BaseService
    {
        public TransferService(ImayPayContext context,
            IMapper mapper, 
            DtoService dtoService,
            TokenService tokenService) 
            : base(context, mapper, dtoService, tokenService){}

        public Transaction Transfer(TransactionDTO transactionDTO, User user)
        {
            if (user == null)
                throw new UnauthorizedAccessException("Usuário não autorizado.");

            var valueTransfer = transactionDTO.ValueTransaction;

            decimal balance = (decimal)_context.Entry(user).Property(u => u.Balance).CurrentValue;


            if (valueTransfer > balance)
                throw new BadHttpRequestException($"Sem saldo: {balance}, valor da transferência {valueTransfer}");

            var userToReceive = _context.Users.FirstOrDefault(u => u.Account == transactionDTO.Account);

            if (userToReceive == null)
                throw new BadHttpRequestException("Usuário de destino é inválido");

            user.Balance -= valueTransfer;
            userToReceive.Balance += valueTransfer;

            var transaction = _dtoService.GetTransactionFromTransactionDTO(transactionDTO);

            // string date = String.Format("{yyyy-MM-dd", transaction.Date);
            if (transaction.Date.Day != DateTime.Today.Day)
                transaction.Status = "Agendada";
            else
                transaction.Status = "Realizada";

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }
    }
}
