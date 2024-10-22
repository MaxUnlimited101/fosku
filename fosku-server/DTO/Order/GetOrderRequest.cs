using System.ComponentModel.DataAnnotations;
using fosku_server.Helpers;

namespace fosku_server.DTO.Order;

public record GetOrderRequest(
    [Range(0, int.MaxValue)]
    int Id
);
