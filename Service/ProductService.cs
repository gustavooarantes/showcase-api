using System.Text.Json;
using FoodNet.DTO;
using FoodNet.Entity;
using FoodNet.Repository;
using Microsoft.Extensions.Caching.Distributed;

namespace FoodNet.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMessageProducer _messageProducer;
    private readonly IDistributedCache _cache;
    private const string PRODUCTS_CACHE_KEY = "products_all";

    public ProductService(
        IProductRepository repository, 
        IMessageProducer messageProducer,
        IDistributedCache cache)
    {
        _repository = repository;
        _messageProducer = messageProducer;
        _cache = cache;
    }

    public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            category = dto.Category
        };

        await _repository.AddAsync(product);

        var responseDto = new ProductResponseDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.category
        );

        _messageProducer.SendMessage(responseDto);

        await _cache.RemoveAsync(PRODUCTS_CACHE_KEY);

        return responseDto;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var cachedProducts = await _cache.GetStringAsync(PRODUCTS_CACHE_KEY);

        if (!string.IsNullOrEmpty(cachedProducts))
        {
            return JsonSerializer.Deserialize<IEnumerable<ProductResponseDto>>(cachedProducts)!;
        }

        var products = await _repository.GetAllAsync();
        var productsDto = products.Select(p => new ProductResponseDto(p.Id, p.Name, p.Description, p.Price, p.category));
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

        var jsonToCache = JsonSerializer.Serialize(productsDto);
        await _cache.SetStringAsync(PRODUCTS_CACHE_KEY, jsonToCache, options);

        return productsDto;
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductResponseDto(product.Id, product.Name, product.Description, product.Price, product.category);
    }
}