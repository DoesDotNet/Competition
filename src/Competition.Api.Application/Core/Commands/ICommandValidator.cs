using FluentValidation.Results;

namespace Shop.Application.Core.Commands;

public interface ICommandValidator<in TCommand>
{
    Task<ValidationResult> ValidateCommand(TCommand command);
}