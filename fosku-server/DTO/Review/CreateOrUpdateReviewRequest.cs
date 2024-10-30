using fosku_server.Helpers.Validation;
using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Review;

public record CreateOrUpdateReviewRequest(
    [Range(0, int.MaxValue)]
    int Id,

    [Range(0, int.MaxValue)]
    int CustomerId,

    [Range(0, int.MaxValue)]
    int ProductId,

    [Range(1, 5, MaximumIsExclusive = false, MinimumIsExclusive = false)]
    int Rating,

    [MaxLength(400)]
    string Comment,

    [DateValidation]
    DateOnly CreatedAt
);

