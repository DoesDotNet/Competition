using Shop.Application.Core.Commands;

namespace Shop.Application.Games.Commands;

public record StartGame(Guid GameId) : ICommand;