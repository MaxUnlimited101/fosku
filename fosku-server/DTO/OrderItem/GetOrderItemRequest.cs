using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.OrderItem;

public record GetOrderItemRequest(
    [Range(0, int.MaxValue)]
    int Id
);