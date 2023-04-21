using System.ComponentModel.DataAnnotations.Schema;

namespace ImaPayAPI.Models
{
    public class Transaction : Entity
    {
        public DateTime Date { get; set; }
        public string Agency { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal ValueTransaction { get; set; }
        public string Receiver { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }   
}
