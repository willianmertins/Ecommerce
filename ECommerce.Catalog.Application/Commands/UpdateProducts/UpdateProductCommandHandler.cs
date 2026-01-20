using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Exceptions;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands.UpdateProducts;

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
                return Result.Failure(Error.NotFound(nameof(product), request.Id));

            product.UpdateDetails(request.Name, request.Description, request.ImageUrl);
            product.UpdatePrice(request.Price);
            product.UpdateStock(request.Stock);

            if (request.CategoryId.HasValue)
            {
                product.AssignCategory(request.CategoryId.Value);
            }
            else
            {
                product.RemoveCategory();
            }

            await productRepository.UpdateAsync(product, cancellationToken);

            return Result.Success();
        }
        catch (DomainException ex)
        {
            return Result.Failure(Error.Validation(ex.Message));
        }
    }
}