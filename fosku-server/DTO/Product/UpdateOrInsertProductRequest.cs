using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Product;

public record UpdateOrInsertProductRequest(
    [Range(0, int.MaxValue)]
    int Id,

    [MaxLength(100)]
    string Name,

    [MaxLength(1000)]
    string Description,

    [Range(0, float.MaxValue)]
    float Price,

    [Range(0, int.MaxValue)]
    int StockQuantity
);