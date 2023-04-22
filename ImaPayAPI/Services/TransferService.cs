using ImaPayAPI.Context;
using ImaPayAPI.Models;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Services.DTO;
using ImaPayAPI.Services.Token;

namespace ImaPayAPI.Services
{
    public class TransferService
    {
        private ImayPayContext _context;
        private DtoService _dtoService;
        private ValidateAndReturnUserService _validateAndReturnUserService;

        public TransferService(ImayPayContext context, DtoService dtoService, ValidateAndReturnUserService validateAndReturnUserService)
        {
            _context = context;
            _dtoService = dtoService;
            _validateAndReturnUserService = validateAndReturnUserService; 
        }

        public Transaction Transfer(TransactionDTO transactionDTO, User user)
        {
            var valueTransfer = transactionDTO.ValueTransaction;

            if (user == null)
                throw new UnauthorizedAccessException("Usuário não autorizado.");

            decimal balance = (decimal)_context.Entry(user).Property(u => u.Balance).CurrentValue;


            if (valueTransfer > balance)
                throw new BadHttpRequestException($"Sem saldo: {balance}, valor da transferência {valueTransfer}");

            user.Balance -= valueTransfer;
            

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
