using Competition.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Competition.Api.Games.GetGame;

[ApiController]
[Route("games/")]
public class GetGameController : ControllerBase
{
    private readonly GameService _gameService;

    public GetGameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameModel>> GetGames(Guid id, CancellationToken cancellationToken)
    {
        var game = await _gameService.GetGame(id, cancellationToken);

        if (game == null)
            return NotFound();

        var model = new GameModel
        {
            Id = game.Id,
            Name = game.Name,
            Prize = game.Prize,
            Winner = game.Winner?.Name
        };
        
        return Ok(model);
    }
}