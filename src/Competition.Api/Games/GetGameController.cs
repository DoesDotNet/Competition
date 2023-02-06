using Microsoft.AspNetCore.Mvc;
using Shop.Application.Core.Queries;
using Shop.Application.Games.Queries;

namespace Competition.Api.Games;

[ApiController]
[Route("games/")]
public class GetGameController : ControllerBase
{
    private readonly IQueryProcessor _queryProcessor;

    public GetGameController(IQueryProcessor queryProcessor)
    {
        _queryProcessor = queryProcessor;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetGameResponse>> GetGames(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetGameDetails(id);
        var game = await _queryProcessor.Process(query, cancellationToken);

        if (game == null)
            return NotFound();

        var model = new GetGameResponse
        {
            Id = game.Id,
            Name = game.Name,
            Prize = game.Prize,
            Winner = game.Winner?.Name
        };
        
        return Ok(model);
    }
}