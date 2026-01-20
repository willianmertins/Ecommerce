using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands.CreateProducts;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string ImageUrl,
    Guid? CategoryId = null
) : IRequest<Result<Guid>>;