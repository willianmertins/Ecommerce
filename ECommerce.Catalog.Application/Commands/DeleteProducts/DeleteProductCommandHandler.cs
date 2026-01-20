using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands.DeleteProducts;

public class DeleteProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            return Result.Failure(Error.NotFound(nameof(Product), request.Id));

        await productRepository.DeleteAsync(product, cancellationToken);

        return Result.Success();
    }
}