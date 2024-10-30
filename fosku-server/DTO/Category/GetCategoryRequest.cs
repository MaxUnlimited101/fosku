using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Category;

public record GetCustomerRequest(
    [Range(0, int.MaxValue)]
    int Id
);
