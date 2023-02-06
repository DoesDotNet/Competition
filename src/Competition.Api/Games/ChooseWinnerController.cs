using Microsoft.AspNetCore.Mvc;
using Shop.Application.Core.Commands;
using Shop.Application.Games.Commands;

namespace Competition.Api.Games;


[ApiController]
[Route("games/")]
public class ChooseWinnerController: ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public ChooseWinnerController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{id}/winner")]
    public async Task<ActionResult> ChooseWinner(Guid id, CancellationToken cancellationToken)
    {
        var command = new ChooseWinner(id);
        await _commandDispatcher.Dispatch(command, cancellationToken);
        
        return Ok();
    }
}