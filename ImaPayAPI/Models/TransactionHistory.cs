using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImaPayAPI.Models
{
    public class TransactionHistory : Entity
    {
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
    }
}
