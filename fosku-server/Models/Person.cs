using System.ComponentModel.DataAnnotations;

namespace fosku_server.Models;

public abstract class Person : IFoskuModel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }

    /// <summary>
    /// SHA256 hash
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string PasswordHash { get; set; }

    [Required]
    [MaxLength(50)]
    public string SaltString { get; set; }

    [MaxLength(20)]
    public string PhoneNumber { get; set; }
}