using System.Data;
using FluentValidation;

namespace ECommerce.Catalog.Application.Commands.UpdateProducts;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product Id cannot be empty");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product Name cannot be empty")
            .MaximumLength(200)
            .WithMessage("Product Name cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Product Description cannot exceed 1000 characters");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product Price cannot be zero or negative");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product Stock cannot be negative");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500)
            .WithMessage("Product ImageUrl cannot exceed 500 characters");
    }
}