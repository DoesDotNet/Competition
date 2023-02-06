using FluentValidation.Results;
using Shop.Application;
using Shop.Application.Core.Commands;

namespace Competition.Api.Infrastructure;

public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly ICommandValidator<TCommand> _validator;
    private readonly ICommandHandler<TCommand> _decoratee;

    public ValidationCommandHandlerDecorator(ICommandValidator<TCommand> validator, ICommandHandler<TCommand> decoratee)
    {
        _validator = validator;
        _decoratee = decoratee;
    }

    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        ValidationResult results = await _validator.ValidateCommand(command);

        if(results.IsValid)
        {
            await _decoratee.Handle(command, cancellationToken);
        }
        else
        {
            throw new GameValidationException(results);
        }              
    }
}