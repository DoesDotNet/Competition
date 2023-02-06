using Microsoft.AspNetCore.Mvc;
using Shop.Application.Core.Commands;
using Shop.Application.Games.Commands;

namespace Competition.Api.Games;

[ApiController]
[Route("games/")]
public class AddEntryController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public AddEntryController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost("{id}/entries")]
    public async Task<ActionResult> AddEntry([FromRoute]Guid id, [FromBody]AddEntryRequest model, CancellationToken cancellationToken)
    {
        var command = new AddEntry(id, model.Name, model.TelephoneNumber);
        await _commandDispatcher.Dispatch(command, cancellationToken);
        return NoContent();
    }
}

public class AddEntryRequest
{
    public string Name { get; set; }
    public string TelephoneNumber { get; set; }
}