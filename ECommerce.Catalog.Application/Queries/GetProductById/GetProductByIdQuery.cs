using ECommerce.Catalog.Application.DTOs;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>;