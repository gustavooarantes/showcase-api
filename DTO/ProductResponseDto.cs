namespace FoodNet.DTO;

public record ProductResponseDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string Category
);