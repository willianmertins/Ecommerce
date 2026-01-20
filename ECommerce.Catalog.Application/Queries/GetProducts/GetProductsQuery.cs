using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Entities;
using ECommerce.Common.Results;
using MediatR;

namespace ECommerce.Catalog.Application.Queries.GetProducts;

public record GetProductsQuery() : IRequest<Result<IEnumerable<ProductDto>>>;