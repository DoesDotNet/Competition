using Microsoft.EntityFrameworkCore;
using Shop.Application;
using Shop.Application.Core.Data;
using Shop.Application.Core.Providers;
using Shop.Application.Domain;

namespace Competition.Api.Services;

public class GameService
{
    private readonly ILogger<GameService> _logger;
    private readonly CompetitionDbContext _db;
    private readonly ISmsSender _smsSender;

    public GameService(ILogger<GameService> logger, CompetitionDbContext db, ISmsSender smsSender)
    {
        _logger = logger;
        _db = db;
        _smsSender = smsSender;
    }

    public async Task CreateNewGame(Guid id, string name, string prize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateNewGame started");

        try
        {
            var game = Game.Create(id, name, prize);

            await _db.Games.AddAsync(game, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "CreateNewGame error");
            throw;
        }
        
        _logger.LogInformation("CreateNewGame completed");
    }

    public async Task<Game?> GetGame(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateNewGame started");

        Game? game;
        
        try
        {
            game = await _db.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "CreateNewGame error");
            throw;
        }
        
        _logger.LogInformation("CreateNewGame completed");

        return game;
    }

    public async Task ChooseWinner(Guid gameId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ChooseWinner started");

        try
        {
            Game? game = await _db.Games.FirstOrDefaultAsync(x => x.Id == gameId, cancellationToken: cancellationToken);
            
            if (game == null || game.IsLive)
            {
                throw new GameValidationException("Game either does not exist or is not live.");
            }
            
            game.ChooseWinner();
            await _db.SaveChangesAsync(cancellationToken);

            await _smsSender.SendSms(game.Winner.TelephoneNumber,
                $"Congratulations you have won a {game.Prize}, in the {game.Name} game.", cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "ChooseWinner error");
            throw;
        }
        
        _logger.LogInformation("ChooseWinner completed");
    }
}