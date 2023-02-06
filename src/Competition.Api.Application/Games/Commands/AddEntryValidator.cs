using FluentValidation;
using Shop.Application.Core.Commands;

namespace Shop.Application.Games.Commands;

public class AddEntryValidator : BaseCommandValidator<AddEntry>
{
    public AddEntryValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.TelephoneNumber)
            .NotEmpty();
    }
}