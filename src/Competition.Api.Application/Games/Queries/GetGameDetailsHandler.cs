using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Data;
using Shop.Application.Core.Queries;
using Shop.Application.Domain;

namespace Shop.Application.Games.Queries;

public class GetGameDetailsHandler : IQueryHandler<GetGameDetails, Game>
{
    private readonly CompetitionDbContext _db;

    public GetGameDetailsHandler(CompetitionDbContext db)
    {
        _db = db;
    }
    
    public async Task<Game?> Handle(GetGameDetails query, CancellationToken cancellationToken)
    {
        return await _db.Games.FirstOrDefaultAsync(cancellationToken);
    }
}