using Microsoft.EntityFrameworkCore;
using FoodNet.Data;
using FoodNet.Entities;

namespace FoodNet.Repository;

public class ProductRepository : IProductRepository
{
    private readonly FoodNetDbContext _context;

    public ProductRepository(FoodNetDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    
    public Task UpdateAsync(Product product) => Task.CompletedTask; 
    public Task DeleteAsync(int id) => Task.CompletedTask;
}