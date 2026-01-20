namespace ECommerce.Catalog.Application.DTOs;

public record CreateProductRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public string ImageUrl { get; set; } = string.Empty;
    public Guid? CategoryId { get; init; }
}