using Shop.Application.Core.Commands;

namespace Shop.Application.Games.Commands;

public record ChooseWinner(Guid GameId) : ICommand;