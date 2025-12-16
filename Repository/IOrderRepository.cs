using FoodNet.Entity;

namespace FoodNet.Repository;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
}