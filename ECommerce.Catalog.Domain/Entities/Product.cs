using ECommerce.Catalog.Domain.Events;
using ECommerce.Common.Exceptions;
using ECommerce.Common.Models;

namespace ECommerce.Catalog.Domain.Entities;

public class Product : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }

    private Product() { }

    public static Product Create(
        string name,
        string description,
        decimal price,
        int stock,
        string imageUrl,
        Guid? categoryId = null)
    {
        ValidateName(name);
        ValidatePrice(price);
        ValidateStock(stock);

        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Stock = stock,
            ImageUrl = imageUrl,
            CategoryId = categoryId
        };

        product.AddDomainEvent(new ProductCreatedEvent(product.Id, product.Name));
        return product;
    }

    public void UpdatePrice(decimal newPrice)
    {
        ValidatePrice(newPrice);

        if (Price == newPrice)
            return;

        var oldPrice = Price;
        Price = newPrice;
        SetUpdatedAt();

        AddDomainEvent(new ProductPriceChangedEvent(Id, oldPrice, newPrice));
    }

    public void UpdateStock(int quantity)
    {
        ValidateStock(quantity);
        Stock = quantity;
        SetUpdatedAt();
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity to add must be greater than zero.");

        Stock += quantity;
        SetUpdatedAt();
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity to remove must be greater than zero.");
        
        if (Stock < quantity)
            throw new DomainException("Insufficient stock to remove the requested quantity.");
        
        Stock -= quantity;
        SetUpdatedAt();
    }

    public void UpdateDetails(
        string name,
        string description,
        string imageUrl)
    {
        ValidateName(name);

        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        SetUpdatedAt();
    }

    public void AssignCategory(Guid? categoryId)
    {
        CategoryId = categoryId;
        SetUpdatedAt();
    }

    public void RemoveCategory()
    {
        CategoryId = null;
        SetUpdatedAt();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty.");

        if (name.Length > 200)
            throw new DomainException("Product name cannot exceed 200 characters.");
    }

    private static void ValidatePrice(decimal price)
    {
        if (price <= 0)
            throw new DomainException("Product price must be greater than zero.");
    }

    private static void ValidateStock(int stock)
    {
        if (stock < 0)
            throw new DomainException("Product stock cannot be negative.");
    }

}
