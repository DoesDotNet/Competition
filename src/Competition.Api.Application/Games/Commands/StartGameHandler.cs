using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;

namespace Shop.Application.Games.Commands;

public class StartGameHandler : ICommandHandler<StartGame>
{
    private readonly CompetitionDbContext _db;

    public StartGameHandler(CompetitionDbContext db)
    {
        _db = db;
    }
    
    public async Task Handle(StartGame command, CancellationToken cancellationToken)
    {
        var game = await _db.Games.FirstOrDefaultAsync(x => x.Id == command.GameId, cancellationToken);
        if (game == null)
            throw new NotFoundException("Game not found.");
        
        game.Start();

        await _db.SaveChangesAsync(cancellationToken);
    }
}