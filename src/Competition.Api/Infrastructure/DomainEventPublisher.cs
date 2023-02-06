using System.Diagnostics;
using Shop.Application.Core.Events;

namespace Competition.Api.Infrastructure;

public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        var wrapperType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());

        var handlers = _serviceProvider.GetServices(handlerType);
        if (handlers == null)
        {
            throw new Exception($"Handler for {domainEvent.GetType()} not found");
        }

        foreach (var handler in handlers)
        {
            if (Activator.CreateInstance(wrapperType, handler) is DomainEventHandler wrappedHandler)
            {
                await wrappedHandler.Handle(domainEvent, cancellationToken);
            }
            else
            {
                // might need to aggregate these errors to process other remaining event handlers
                throw new Exception("Handler creation error");
            }
        }
    }
    
    private abstract class DomainEventHandler
    {
        public abstract Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }

    private class DomainEventHandler<T> : DomainEventHandler
        where T : IDomainEvent
    {
        private readonly IDomainEventHandler<T> _handler;

        public DomainEventHandler(IDomainEventHandler<T> handler)
        {
            _handler = handler;
        }

        [DebuggerStepThrough]
        public override Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return _handler.Handle((T)domainEvent, cancellationToken);
        }
    }
}