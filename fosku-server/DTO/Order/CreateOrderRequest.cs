using System.ComponentModel.DataAnnotations;
using fosku_server.Helpers;

namespace fosku_server.DTO.Order;

public record CreateOrderRequest(

    [MaxLength(100)]
    string Name,

    [MaxLength(1000)]
    string Description,

    [Range(0, float.MaxValue)]
    float Price,

    [Range(0, int.MaxValue)]
    int StockQuantity,

    [Range(0, int.MaxValue)]
    int CategoryId,

    [EnumDataType(typeof(OrderStatusEnum))]
    OrderStatusEnum OrderStatus
);
