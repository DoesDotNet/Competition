using FluentValidation;
using FluentValidation.Results;

namespace Shop.Application.Core.Commands;

public class BaseCommandValidator<TCommand> : AbstractValidator<TCommand>, ICommandValidator<TCommand>
    where TCommand : ICommand
{
    public ValidationResult ValidateCommand(TCommand command)
    {
        return Validate(command);
    }
}