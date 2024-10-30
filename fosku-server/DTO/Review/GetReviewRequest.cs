using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.Review;

public record GetReviewRequest(
    [Range(0, int.MaxValue)]
    int Id
);
