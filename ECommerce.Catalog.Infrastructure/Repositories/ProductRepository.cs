using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Domain.Repositories;
using ECommerce.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Catalog.Infrastructure.Repositories;

public class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context
            .Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context
            .Products
            .Include(p => p.Category)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await context.Products
            .Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Products.AnyAsync(p => p.Id == id, cancellationToken);
    }
}