using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using fosku_server.Models;

namespace fosku_server.DTO.Product;

public record CreateProductRequest(
    [MaxLength(100)]
    string Name,

    [MaxLength(1000)]
    string Description,

    [Range(0, float.MaxValue)]
    float Price,

    [Range(0, int.MaxValue)]
    int StockQuantity,

    [MaxLength(200)]
    string LogoUrl,

    [MaxLength(100)]
    string AltText,

    List<Models.ProductImage> ProductImages
);