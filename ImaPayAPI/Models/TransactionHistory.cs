namespace ImaPayAPI.Models
{
    public class TransactionHistory : Entity
    {
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public decimal Value { get; set; }
    }
}
