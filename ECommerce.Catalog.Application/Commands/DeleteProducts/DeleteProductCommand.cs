using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Commands.DeleteProducts;

public record DeleteProductCommand(Guid Id) : IRequest<Result>;