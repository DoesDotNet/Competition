using System.Diagnostics;
using Shop.Application.Core.Commands;

namespace Competition.Api.Infrastructure;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(ICommand command, CancellationToken cancellationToken)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
        var wrapperType = typeof(CommandHandler<>).MakeGenericType(command.GetType());

        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
        {
            throw new Exception($"Handler for {command.GetType()} not found");
        }

        if (Activator.CreateInstance(wrapperType, handler) is CommandHandler wrappedHandler)
        {
            await wrappedHandler.Handle(command, cancellationToken);
        }
        else
        {
            throw new Exception("Handler creation error");
        }
    }
    
    private abstract class CommandHandler
    {
        public abstract Task Handle(ICommand command, CancellationToken cancellationToken);
    }

    private class CommandHandler<T> : CommandHandler
        where T : ICommand
    {
        private readonly ICommandHandler<T> _handler;

        public CommandHandler(ICommandHandler<T> handler)
        {
            _handler = handler;
        }

        [DebuggerStepThrough]
        public override Task Handle(ICommand command, CancellationToken cancellationToken)
        {
            return _handler.Handle((T)command, cancellationToken);
        }
    }
}