using ImaPayAPI.Context;
using ImaPayAPI.Models.DTO;
using ImaPayAPI.Models;
using System.Transactions;
using System.Net.WebSockets;
using ImaPayAPI.Services.DTO;
using AutoMapper;
using ApiAuth.Services;

namespace ImaPayAPI.Services
{
    public class TransferHistoryService : BaseService
    {
        public TransferHistoryService(ImayPayContext context,
            IMapper mapper,
            DtoService dtoService,
            TokenService tokenService)
            : base(context, mapper, dtoService, tokenService) { }

        public TransferHistoryDTO GetTransferHistory(string token)
        {
            var user = _tokenService.Validate(token);

            if (user == null)
                throw new UnauthorizedAccessException("Usuário não autorizado.");


            var transactions = _context.Transactions
            .Where(t => t.UserId == user.Id || t.ReceiverId == user.Id)
            .OrderByDescending(t => t.Date)
            .Select( t => new Models.Transaction
            {
                Account = t.Account,
                Agency = t.Agency,
                Date = t.Date,
                AccountType = t.AccountType,
                Status = t.Status,
                User = t.User,
                UserId = t.UserId,
                ReceiverId = t.ReceiverId,
                Receiver = t.Receiver,
                ValueTransaction = (t.ReceiverId == user.Id)? t.ValueTransaction : t.ValueTransaction * -1
            })
            .ToList();

            var transferHistory = _dtoService.GetTransactionHistoryDTO(transactions);

            return transferHistory;
        }
    }
}
