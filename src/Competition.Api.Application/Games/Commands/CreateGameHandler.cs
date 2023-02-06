using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;
using Shop.Application.Domain;

namespace Shop.Application.Games.Commands;

public class CreateGameHandler : ICommandHandler<CreateGame>
{
    private readonly CompetitionDbContext _db;

    public CreateGameHandler(CompetitionDbContext db)
    {
        _db = db;
    }
    
    public async Task Handle(CreateGame command, CancellationToken cancellationToken)
    {
        var game = Game.Create(command.Id, command.Name, command.Prize);

        await _db.Games.AddAsync(game, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }
}