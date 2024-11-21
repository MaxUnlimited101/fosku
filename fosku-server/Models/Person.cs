using System.ComponentModel.DataAnnotations;

namespace fosku_server.Models;

public abstract class Person : IFoskuModel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string SaltString { get; set; } = null!;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; } = null!;
}