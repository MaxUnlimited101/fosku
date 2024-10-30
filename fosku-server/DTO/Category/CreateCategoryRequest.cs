using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Category;

public record CreateCategoryRequest(
    [MaxLength(100)]
    string Name,

    [MaxLength(400)]
    string Description,

    [Range(0, int.MaxValue)]
    int ParentCategoryId
);