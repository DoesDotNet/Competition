using Competition.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Core.Commands;

namespace Competition.Api.Games.ChooseWinner;


[ApiController]
[Route("games/")]
public class ChooseWinnerController: ControllerBase
{
    private readonly GameService _gameService;
    private readonly ICommandDispatcher _commandDispatcher;

    public ChooseWinnerController(GameService gameService, ICommandDispatcher commandDispatcher)
    {
        _gameService = gameService;
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> ChooseWinner(Guid id, CancellationToken cancellationToken)
    {

        var command = new Shop.Application.Games.Commands.ChooseWinner(id);
        await _commandDispatcher.Dispatch(command, cancellationToken);
        
        
        return Ok();
    }
}