using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.ProductImage;

public record CreateProductImageRequest(
    [MaxLength(100)]
    string ImageUrl,

    [MaxLength(100)]
    string AltText,

    [Range(0, int.MaxValue)]
    int ProductId
);
