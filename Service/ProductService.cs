using FoodNet.DTO;
using FoodNet.Entity;
using FoodNet.Repository;

namespace FoodNet.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMessageProducer _messageProducer;

    public ProductService(IProductRepository repository, IMessageProducer messageProducer)
    {
        _repository = repository;
        _messageProducer = messageProducer;
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

        return responseDto;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllAsync();
        return products.Select(p => new ProductResponseDto(p.Id, p.Name, p.Description, p.Price, p.category));
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductResponseDto(product.Id, product.Name, product.Description, product.Price, product.category);
    }
}