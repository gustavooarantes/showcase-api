using FoodNet.DTO;

namespace FoodNet.Service;

public interface IProductService
{
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto dto);
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto?> GetProductByIdAsync(int id);
}