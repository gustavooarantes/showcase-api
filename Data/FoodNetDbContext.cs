using FoodNet.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodNet.Data;

public class FoodNetDbContext : DbContext
{
    public FoodNetDbContext(DbContextOptions<FoodNetDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}