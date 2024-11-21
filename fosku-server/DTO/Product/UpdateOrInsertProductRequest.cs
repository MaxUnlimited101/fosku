using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Product;

public record UpdateOrInsertProductRequest(
    [Range(0, int.MaxValue)]
    int id,

    [MaxLength(100)]
    string name,

    [MaxLength(1000)]
    string description,

    [Range(0, float.MaxValue)]
    float price,

    [Range(0, int.MaxValue)]
    int stockQuantity,

    [MaxLength(200)]
    string logoUrl,

    [MaxLength(100)]
    string logoAltText
);