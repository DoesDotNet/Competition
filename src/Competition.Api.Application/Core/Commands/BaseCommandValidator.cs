using FluentValidation;
using FluentValidation.Results;

namespace Shop.Application.Core.Commands;

public class BaseCommandValidator<TCommand> : AbstractValidator<TCommand>, ICommandValidator<TCommand>
    where TCommand : ICommand
{
    public Task<ValidationResult> ValidateCommand(TCommand command)
    {
        return ValidateAsync(command);
    }
}