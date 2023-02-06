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
    
    public Task Handle(ChooseWinner command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}