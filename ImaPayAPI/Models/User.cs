using System.ComponentModel.DataAnnotations;

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
    public decimal? Balance { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    
}
