using Microsoft.AspNetCore.Mvc;
using Shop.Application.Core.Commands;
using Shop.Application.Games.Commands;

namespace Competition.Api.Games;

[ApiController]
[Route("games/")]
public class CreateGameController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CreateGameController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> CreateGame(CreateGameModel model, CancellationToken cancellationToken)
    {
        var command = new CreateGame(model.Id, model.Name, model.Prize);
        await _commandDispatcher.Dispatch(command, cancellationToken);
        return Created($"game/{model.Id}", null);
    }
}