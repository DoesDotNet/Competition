using Shop.Application.Core.Queries;
using Shop.Application.Domain;

namespace Shop.Application.Games.Queries;

public record GetGameDetails(Guid Id) : IQuery<Game>;