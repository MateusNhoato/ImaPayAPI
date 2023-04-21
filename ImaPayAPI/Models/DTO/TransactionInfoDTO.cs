namespace ImaPayAPI.Models.DTO
{
    public class TransactionInfoDTO
    {
        public DateTime Date { get; set; }
        public string Agency { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal ValueTransaction { get; set; }
        public string Status { get; set; }
    }
}
