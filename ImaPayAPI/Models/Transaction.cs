﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ImaPayAPI.Models
{
    public class Transaction : Entity
    {
        public DateTime Date { get; set; }
        public string Agency { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValueTransaction { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [NotMapped] 
        public User Receiver { get; set; }
        public int ReceiverId { get; set; }
    }   
}
