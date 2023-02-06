using FluentValidation;
using Shop.Application.Core.Commands;

namespace Shop.Application.Games.Commands;

public class CreateGameValidator : BaseCommandValidator<CreateGame>
{
    public CreateGameValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty();

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();
        
        RuleFor(c => c.Prize)
            .NotEmpty()
            .NotNull();
    }        
}