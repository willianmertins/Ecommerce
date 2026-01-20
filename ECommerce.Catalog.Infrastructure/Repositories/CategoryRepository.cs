using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infrastructure.Repositories;

public class CategoryRepository(CatalogDbContext context) : ICategoryRepository
{
    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken)
    {
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}