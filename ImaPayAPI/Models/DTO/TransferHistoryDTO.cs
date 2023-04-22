using ImaPayAPI.Models;

namespace ImaPayAPI.Models.DTO
{
    public class TransferHistoryDTO
    {
        public List<Transaction> Transactions { get; set; }
    }
}