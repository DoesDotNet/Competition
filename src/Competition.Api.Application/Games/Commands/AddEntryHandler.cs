using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;
using Shop.Application.Domain;

namespace Shop.Application.Games.Commands;

public class AddEntryHandler : ICommandHandler<AddEntry>
{
    private readonly CompetitionDbContext _db;

    public AddEntryHandler(CompetitionDbContext db)
    {
        _db = db;
    }
    
    public async Task Handle(AddEntry command, CancellationToken cancellationToken)
    {
        var game = await _db.Games.FirstOrDefaultAsync(x => x.Id == command.GameId, cancellationToken);
        if (game == null)
            throw new NotFoundException($"Game with Id {command.GameId}, not found.");
        
        game.AddEntry(new Entry(command.Name, command.TelephoneNumber));
        await _db.SaveChangesAsync(cancellationToken);
    }
}