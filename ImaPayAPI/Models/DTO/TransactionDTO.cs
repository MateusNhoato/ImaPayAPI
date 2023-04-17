namespace ImaPayAPI.Models.DTO
{
    public class TransactionDTO
    {
        public DateTime Date { get; set; }
        public string Agency { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal ValueTransaction { get; set; }
    }
}
