using ImaPayAPI.Models;

namespace ImaPayAPI.Models.DTO
{
    public class TransferHistoryDTO
    {
        public List<TransactionInfoDTO> Transactions { get; set; }
    }
}