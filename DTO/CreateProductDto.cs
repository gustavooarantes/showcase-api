using System.ComponentModel.DataAnnotations;

namespace FoodNet.DTO;

public record CreateProductDto(
    [Required] string Name,
    string Description,
    [Range(0.01, 10000)] decimal Price,
    string Category
);