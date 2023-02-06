using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Events;
using Shop.Application.Domain;

namespace Shop.Application.Core.Data;

public class CompetitionDbContext : DbContext
{
    private readonly IDomainEventPublisher _domainEventPublisher;
    public DbSet<Game> Games { get; set; }

    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options, IDomainEventPublisher domainEventPublisher)
        : base(options)
    {
        _domainEventPublisher = domainEventPublisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.OwnsMany(e => e.Entries);
            entity.OwnsOne(e => e.Winner, w => { })
                .Navigation(n => n.Winner)
                .IsRequired();
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await PublishEvents(cancellationToken);
        return await base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        await PublishEvents(cancellationToken);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private async Task PublishEvents(CancellationToken cancellationToken)
    {
        // Here we are handling events before we actually call the SaveChanges method. This will mean
        // that if we change the system state in an event handler it will be persisted in the same transaction.
        // This might not be the desired effect.
        
        // If you have event that needs to be published to a bus of some sort then you could instead save
        // the events to the database and use the outbox pattern, this is more resilient than sending directly
        // from an event handler.

        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities
            .ForEach(entity => entity.Entity.ClearEvents());

        foreach (var domainEvent in domainEvents)
            await _domainEventPublisher.Publish(domainEvent, cancellationToken);
    }
}