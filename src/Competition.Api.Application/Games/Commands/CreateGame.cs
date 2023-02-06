using Shop.Application.Core.Commands;

namespace Shop.Application.Games.Commands;

public record CreateGame(Guid Id, string Name, string Prize) : ICommand;