using Competition.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Competition.Api.Games.CreateGame;

[ApiController]
[Route("games/")]
public class CreateGameController : ControllerBase
{
    private readonly GameService _gameService;

    public CreateGameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateGame(CreateGameModel model, CancellationToken cancellationToken)
    {
        await _gameService.CreateNewGame(model.Id, model.Name, model.Prize, cancellationToken);
        return Created($"game/{model.Id}", null);
    }
}