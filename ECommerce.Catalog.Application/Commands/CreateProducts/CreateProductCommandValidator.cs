using FluentValidation;

namespace ECommerce.Catalog.Application.Commands.CreateProducts;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name must not exceed 100 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Product description must not exceed 1000 characters.");
        
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product price must be greater than or equal to 0.");
        
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product stock must be greater than or equal to 0.");
        
        RuleFor(x => x.ImageUrl)
            .MaximumLength(500)
            .WithMessage("Product image URL must not exceed 500 characters.");
    }
}