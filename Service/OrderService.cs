using FoodNet.DTOs;
using FoodNet.Entity;
using FoodNet.Entity.Enum;
using FoodNet.Repository;

namespace FoodNet.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CustomerName = dto.CustomerName,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            Items = new List<OrderItem>()
        };

        decimal calculatedTotal = 0;

        foreach (var itemDto in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
            
            if (product == null)
            {
                throw new Exception($"Product with ID {itemDto.ProductId} not found.");
            }

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            };

            calculatedTotal += (product.Price * itemDto.Quantity);
            order.Items.Add(orderItem);
        }
        order.TotalPrice = calculatedTotal;

        await _orderRepository.AddAsync(order);

        return MapToDto(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(MapToDto);
    }

    public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order == null ? null : MapToDto(order);
    }

    private static OrderResponseDto MapToDto(Order order)
    {
        return new OrderResponseDto(
            order.Id,
            order.CustomerName,
            order.Status.ToString(),
            order.TotalPrice,
            order.CreatedAt,
            order.Items.Select(i => new OrderItemDto(
                i.Product?.Name ?? "Unknown Product",
                i.Quantity,
                i.UnitPrice,
                i.UnitPrice * i.Quantity
            )).ToList()
        );
    }
}