using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;

namespace Shop.Application.Games.Commands;

public class ChooseWinnerValidator: BaseCommandValidator<ChooseWinner>
{
    private readonly CompetitionDbContext _db;

    public ChooseWinnerValidator(CompetitionDbContext db)
    {
        _db = db;
        
        RuleFor(c => c.GameId)
            .NotEmpty()
            .MustAsync(CheckGameExistsAndIsLive)
            .WithMessage("Game does not exist or is not live");
    }

    private async Task<bool> CheckGameExistsAndIsLive(Guid gameId, CancellationToken cancellationToken)
    {
        return await _db.Games.AnyAsync(x => x.Id == gameId && x.IsLive, cancellationToken: cancellationToken);
    }
}