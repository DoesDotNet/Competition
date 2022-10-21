using FluentValidation.Results;

namespace Shop.Application.Core.Commands;

public interface ICommandValidator<in ICommand>
{
    ValidationResult ValidateCommand(ICommand command);
}