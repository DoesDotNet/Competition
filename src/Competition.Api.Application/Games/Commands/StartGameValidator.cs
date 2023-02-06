using FluentValidation;
using Shop.Application.Core.Commands;
using Shop.Application.Core.Data;

namespace Shop.Application.Games.Commands;

public class StartGameValidator : BaseCommandValidator<StartGame>
{
    private readonly CompetitionDbContext _db;

    public StartGameValidator(CompetitionDbContext db)
    {
        _db = db;
        
        RuleFor(c => c.GameId)
            .NotEmpty();
    }
}