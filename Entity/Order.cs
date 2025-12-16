using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodNet.Entity.Enum;

namespace FoodNet.Entity;

public class Order
{
    [Key]
    public int Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}