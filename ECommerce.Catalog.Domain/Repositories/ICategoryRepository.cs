using ECommerce.Catalog.Domain.Entities;

namespace ECommerce.Catalog.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Category> AddAsync(Category category, CancellationToken cancellationToken);
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    Task DeleteAsync(Category category, CancellationToken cancellationToken);
    Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken);
}