using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Exceptions;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands.CreateProducts;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = Product.Create(
                name: request.Name,
                description: request.Description,
                price: request.Price,
                stock: request.Stock,
                imageUrl: request.ImageUrl,
                categoryId: request.CategoryId);

            await productRepository.AddAsync(product, cancellationToken);
            
            return Result.Success(product.Id);
        }
        catch (DomainException ex)
        {
            return Result.Failure<Guid>(Error.Validation(ex.Message));
        }
    }
}