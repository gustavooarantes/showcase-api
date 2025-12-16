using FoodNet.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodNet.Data;

public class FoodNetDbContext : DbContext
{
    public FoodNetDbContext(DbContextOptions<FoodNetDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
}