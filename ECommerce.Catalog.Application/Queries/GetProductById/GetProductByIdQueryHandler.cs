using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            return Result.Failure<ProductDto>(Error.NotFound(nameof(Product), request.Id));

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Category = product.Category is not null
                ? new CategoryDto
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name,
                    Description = product.Category.Description
                }
                : null,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
        return Result.Success(productDto);
    }
}