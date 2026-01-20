using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            ImageUrl = p.ImageUrl, 
            CategoryId = p.CategoryId,
            Category = p.Category is not null ? new CategoryDto
            {
                Id = p.Category.Id,
                Name = p.Category.Name,
                Description = p.Category.Description
            } : null,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
        
        return Result.Success(productDtos);
    }
}