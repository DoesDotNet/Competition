using Shop.Application.Core.Events;

namespace Shop.Application.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public List<IDomainEvent> DomainEvents => _domainEvents;

    protected void AddEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearEvents()
    {
        _domainEvents?.Clear();
    }   
}
