using System.ComponentModel.DataAnnotations;

namespace FoodNet.DTOs;

public record CreateOrderItemDto(
    [Required] int ProductId,
    [Range(1, 100)] int Quantity
);

public record CreateOrderDto(
    [Required] string CustomerName,
    [Required] List<CreateOrderItemDto> Items
);

public record OrderItemDto(
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal
);

public record OrderResponseDto(
    int Id,
    string CustomerName,
    string Status,
    decimal TotalPrice,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);