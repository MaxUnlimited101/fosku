using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Customer;

public record LoginCustomerRequest(
    [EmailAddress]
    string Email,

    [RegularExpression(@"^[A-Za-z0-9!@#$%^&*()\-_=+]{0,50}$")]
    string Password
);