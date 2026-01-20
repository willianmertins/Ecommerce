using ECommerce.Catalog.Application.DTOs;
using ECommerce.Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController(ICategoryRepository categoryRepository, ILogger<CategoriesController> logger)
    : ControllerBase
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of Categories</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");

        var categories = await categoryRepository.GetAllAsync(cancellationToken);

        var result = categories.Select(c => new
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt,
            ProductCount = c.Products.Count
        });

        return Ok(result);
    }

    /// <summary>
    /// Get category by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Category details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting category by id: {CategoryId}", id);

        var category = await categoryRepository.GetByIdAsync(id, cancellationToken);

        if (category is null)
            return NotFound();

        var result = new
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            Products = category.Products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock
            })
        };

        return Ok(result);
    }
}