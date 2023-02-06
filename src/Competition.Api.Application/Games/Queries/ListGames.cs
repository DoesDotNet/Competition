using Shop.Application.Core.Queries;
using Shop.Application.Domain;

namespace Shop.Application.Games.Queries;

public record ListGames(int Page = 1, int PageSize = 10) : IQuery<IEnumerable<Game>>;