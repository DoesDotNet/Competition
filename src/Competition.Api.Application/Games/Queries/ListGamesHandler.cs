using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Data;
using Shop.Application.Core.Queries;
using Shop.Application.Domain;

namespace Shop.Application.Games.Queries;

public class ListGamesHandler : IQueryHandler<ListGames, IEnumerable<Game>>
{
    private readonly CompetitionDbContext _db;

    public ListGamesHandler(CompetitionDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Game>?> Handle(ListGames query, CancellationToken cancellationToken)
    {
        return await _db.Games
            .OrderBy(x => x.TimeStarded)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}