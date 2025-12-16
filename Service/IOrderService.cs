using FoodNet.DTOs;

namespace FoodNet.Service;

public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<OrderResponseDto?> GetOrderByIdAsync(int id);
}