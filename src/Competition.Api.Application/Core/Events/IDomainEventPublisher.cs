namespace Shop.Application.Core.Events;

public interface IDomainEventPublisher
{
    Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
}