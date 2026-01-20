using ECommerce.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Catalog.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Map to table
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(c => c.Description)
            .HasMaxLength(500);
        builder.Property(c => c.CreatedAt)
            .IsRequired();
        builder.Property(c => c.UpdatedAt)
            .IsRequired(false);
        
        // Configure relationships
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Índices
        builder.HasIndex(c => c.Name).IsUnique();
        
        // Ignorar
        builder.Ignore(c => c.DomainEvents);
    }
}