using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using fosku_server.Models;

namespace fosku_server.DTO.Product;

public record CreateProductRequest(
    [MaxLength(100)]
    [Required]
    string Name,

    [MaxLength(1000)]
    [Required]
    string Description,

    [Range(0, float.MaxValue)]
    [Required]
    float Price,

    [Range(0, int.MaxValue)]
    int StockQuantity,

    [MaxLength(100)]
    string LogoAltText
);