using Microsoft.AspNetCore.Mvc;
using Shop.Application.Core.Commands;
using Shop.Application.Games.Commands;

namespace Competition.Api.Games;

[ApiController]
[Route("games/")]
public class StartGameController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public StartGameController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{id}/start")]
    public async Task<ActionResult> StartGame(Guid id, CancellationToken cancellationToken)
    {
        var command = new StartGame(id);
        await _commandDispatcher.Dispatch(command, cancellationToken);

        return NoContent();
    }
}