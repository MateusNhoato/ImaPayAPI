using System.ComponentModel.DataAnnotations;

namespace ImaPayAPI.Models;

public class User : Entity
{
    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [MaxLength(20)]
    public string Cpf { get; set; } = string.Empty;
    [Required]
    [MaxLength(20)]
    public string Telephone { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MaxLength(20)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [MaxLength(20)]
    public string Account { get; set; } = string.Empty;
    [Required]
    public int Agency { get; set; }
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    [Required]
    public decimal Balance { get; set; }
    public bool IsActive { get; set; } = true;
    
}
