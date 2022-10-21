using FluentValidation;
using Shop.Application.Core.Commands;

namespace Shop.Application.Products.Commands;

public class CreateProductValidator : BaseCommandValidator<CreateProduct>
{
    public CreateProductValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();
    }        
}