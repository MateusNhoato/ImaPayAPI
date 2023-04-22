using ImaPayAPI.Context;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Models;
using System.Transactions;
using System.Net.WebSockets;
using ImaPayAPI.Services.DTO;

namespace ImaPayAPI.Services
{
    public class TransferHistoryService
    {
        private ImayPayContext _context;
        private DtoService _dtoService;

        public TransferHistoryService(ImayPayContext context, DtoService dtoService)
        {
            _context = context;
            _dtoService = dtoService;
        }

        public TransferHistoryDTO GetTransferHistory(string token) 
        {
            var user = _context.userToken[token];

            if (user == null)
                throw new UnauthorizedAccessException("Usuário não autorizado.");


            List<Models.Transaction> transactions = _context.Transactions
                .Where(t => t.Id == user.Id)
                .OrderByDescending(t => t.Date)
                .ToList();

            var transferHistory = _dtoService.GetTransactionHistoryDTO(transactions);

            return transferHistory;
        }
    }
}
