using System.ComponentModel.DataAnnotations;

namespace fosku_server.DTO.OrderItem;

public record UpdateOrInsertOrderItemRequest(
    [Range(0, int.MaxValue)]
    int Id,

    [Range(0, int.MaxValue)]
    int OrderId,

    [Range(0, int.MaxValue)]
    int ProductId,

    [Range(0, int.MaxValue)]
    int Quantity,

    [Range(0, int.MaxValue)]
    int UnitPrice
);
