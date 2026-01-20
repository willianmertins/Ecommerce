using ECommerce.Catalog.Application.Commands.CreateProducts;
using ECommerce.Catalog.Application.Commands.DeleteProducts;
using ECommerce.Catalog.Application.Commands.UpdateProducts;
using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Application.Queries.GetProductById;
using ECommerce.Catalog.Application.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : ControllerBase
{
    /// <summary>
    /// Gets all products.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");

        var query = new GetProductsQuery();
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// Get product by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Product details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product by id: {ProductId}", id);

        var query = new GetProductByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new product.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created product ID</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new product: {ProductName}", request.Name);

        var command = new CreateProductCommand(
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.ImageUrl,
            request.CategoryId
        );

        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Update an existing product.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product: {ProductId}", id);

        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.ImageUrl,
            request.CategoryId
        );

        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.Code.Contains("NotFound", StringComparison.OrdinalIgnoreCase)
                ? NotFound(result.Error)
                : BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product: {ProductId}", id);

        var command = new DeleteProductCommand(id);
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure)
            return NotFound();

        return NoContent();
    }
}