using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands.UpdateProducts;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string ImageUrl,
    Guid? CategoryId) : IRequest<Result>;