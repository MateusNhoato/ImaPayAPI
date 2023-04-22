namespace ImaPayAPI.Models.DTO
{
    public class TransactionDTO
    {
        public DateTime? Date { get; set; } = null!;
        public string Agency { get; set; } = null!;
        public string Account { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public decimal ValueTransaction { get; set; }
    }
}
