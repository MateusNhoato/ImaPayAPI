using ImaPayAPI.Models;

namespace ImaPayAPI.Models.DTO
{
    internal class TransferHistoryDTO
    {
        public List<Transaction> Transactions { get; set; }
    }
}