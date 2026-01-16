using ECommerce.Common.Exceptions;
using ECommerce.Common.Models;

namespace ECommerce.Catalog.Domain.Entities;

public class Category : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Category() { }

    public static Category Create(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Category name cannot be null or empty.");

        return new Category
        {
            Name = name,
            Description = description
        };
    }

    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Category name cannot be null or empty.");

        Name = name;
        Description = description;
        SetUpdatedAt();
    }
}
