using FoodNet.DTO;
using FoodNet.Entities;
using FoodNet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FoodNet.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        var products = await _repository.GetAllAsync();

        var productsDto = products.Select(p => new ProductResponseDto(
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.category
        ));

        return Ok(productsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var productDto = new ProductResponseDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.category
        );

        return Ok(productDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> CreateProduct(CreateProductDto request)
    {
        var newProduct = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            category = request.Category
        };

        await _repository.AddAsync(newProduct);

        var responseDto = new ProductResponseDto(
            newProduct.Id,
            newProduct.Name,
            newProduct.Description,
            newProduct.Price,
            newProduct.category
        );

        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, responseDto);
    }
}