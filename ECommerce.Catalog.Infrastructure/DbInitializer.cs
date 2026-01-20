using ECommerce.Catalog.Domain.Entities;
using ECommerce.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Catalog.Infrastructure;

public class DbInitializer(CatalogDbContext context, ILogger<DbInitializer> logger)
{
    public async Task InitializeAsync()
    {
        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                logger.LogInformation("Migrating database...");
                await context.Database.MigrateAsync();
                logger.LogInformation("Database migration completed.");
            }

            await SeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }

    private async Task SeedAsync()
    {
        if (await context.Categories.AnyAsync())
        {
            logger.LogInformation("Data already contains data. Skipping seed");
            return;
        }

        logger.LogInformation("Seeding database...");

        var electronics = Category.Create("Electronics", "Electronic devices and gadgets");
        var books = Category.Create("Books", "Various kinds of books and literature");
        var clothing = Category.Create("Clothing", "Apparel and accessories");
        var home = Category.Create("Home & Kitchen", "Home appliances and kitchenware");

        await context.Categories.AddRangeAsync(electronics, books, clothing, home);
        await context.SaveChangesAsync();

        var products = new List<Product>
        {
            Product.Create(
                "Laptop Dell Inspiron 15",
                "15.6-inch laptop with Intel Core i5, 8GB RAM, 256GB SSD",
                3499.99m,
                15,
                "https://via.placeholder.com/300x300? text=Laptop",
                electronics.Id
            ),
            Product.Create(
                "Smartphone Samsung Galaxy S23",
                "Latest Samsung flagship with 128GB storage",
                4999.99m,
                25,
                "https://via.placeholder.com/300x300? text=Smartphone",
                electronics.Id
            ),
            Product.Create(
                "Wireless Mouse Logitech MX Master 3",
                "Ergonomic wireless mouse for productivity",
                449.99m,
                50,
                "https://via.placeholder.com/300x300? text=Mouse",
                electronics.Id
            ),
            Product.Create(
                "Clean Code by Robert Martin",
                "A handbook of agile software craftsmanship",
                89.90m,
                100,
                "https://via.placeholder.com/300x300? text=Book",
                books.Id
            ),
            Product.Create(
                "Domain-Driven Design by Eric Evans",
                "Tackling complexity in the heart of software",
                129.90m,
                75,
                "https://via.placeholder.com/300x300?text=Book",
                books.Id
            ),
            Product.Create(
                "Men's Cotton T-Shirt",
                "Comfortable 100% cotton t-shirt in various colors",
                49.90m,
                200,
                "https://via.placeholder.com/300x300? text=TShirt",
                clothing.Id
            ),
            Product.Create(
                "LED Desk Lamp",
                "Adjustable LED lamp with touch control",
                159.90m,
                40,
                "https://via.placeholder.com/300x300? text=Lamp",
                home.Id
            ),
            Product.Create(
                "Coffee Maker",
                "Programmable coffee maker with thermal carafe",
                299.90m,
                30,
                "https://via.placeholder.com/300x300? text=CoffeeMaker",
                home.Id
            )
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        logger.LogInformation(
            "Database seeded successfully with {CategoryCount} categories and {ProductCount} products.",
            await context.Categories.CountAsync(),
            await context.Products.CountAsync());
    }
}