using Microsoft.EntityFrameworkCore;
using Shop.Application.Domain;

namespace Shop.Application.Core.Data;

public class CompetitionDbContext : DbContext
{
    public DbSet<Game> Games { get; set; }

    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options)
        : base(options)
    {
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
}