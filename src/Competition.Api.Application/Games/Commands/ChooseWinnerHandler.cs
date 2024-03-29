using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;

namespace Shop.Application.Games.Commands;

public class ChooseWinnerHandler : ICommandHandler<ChooseWinner>
{
    private readonly CompetitionDbContext _db;

    public ChooseWinnerHandler(CompetitionDbContext db)
    {
        _db = db;
    }

    public async Task Handle(ChooseWinner command, CancellationToken cancellationToken)
    {
        var game = await _db.Games.FirstOrDefaultAsync(x => x.Id == command.GameId, cancellationToken);
        if (game == null)
            throw new NotFoundException("Game not found.");

        game.ChooseWinner();

        await _db.SaveChangesAsync(cancellationToken);
    }
}