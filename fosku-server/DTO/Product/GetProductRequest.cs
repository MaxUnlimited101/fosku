using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Product;

public record GetProductRequest(
    [Range(0, int.MaxValue)]
    int Id
);