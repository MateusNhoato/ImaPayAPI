﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImaPayAPI.Models;

public class User : Entity
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string Cpf { get; set; } =  null!;
    [Required]
    [MaxLength(20)]
    public string Telephone { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string Password { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string Account { get; set; } = null!;
    [Required]
    public int? Agency { get; set; } = null!;
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = null!;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Balance { get; set; } = null!;
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Investments { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Savings { get; set; }

    public bool IsActive { get; set; } = true;
    [NotMapped]
    public ICollection<Transaction> Transactions { get; set; }
}
