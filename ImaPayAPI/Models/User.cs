using System.ComponentModel.DataAnnotations;

namespace ImaPayAPI.Models;

public class User
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Cpf { get; set; } = string.Empty;
    [Required]
    public string Telephone { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Account { get; set; } = string.Empty;
    [Required]
    public int Agency { get; set; }
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public decimal Balance { get; set; }
    
}
