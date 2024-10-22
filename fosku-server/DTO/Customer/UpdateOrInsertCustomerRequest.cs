using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Customer;

public record UpdateOrInsertCustomerRequest(
    [Range(0, int.MaxValue)]
    int Id,

    [MaxLength(100)]
    string FirstName,

    [MaxLength(100)]
    string LastName,

    [EmailAddress]
    string Email,

    [RegularExpression(@"^[A-Za-z0-9!@#$%^&*()\-_=+]{0,50}$")]
    string Password,

    [Phone]
    string PhoneNumber,

    [MaxLength(100)]
    string Address,

    [MaxLength(50)]
    string City,

    [MaxLength(50)]
    string Country
);