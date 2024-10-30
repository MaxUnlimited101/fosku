using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.ProductImage;

public record GetProductImageRequest(
    [Range(0, int.MaxValue)]
    int Id
);
