using Shop.Application.Core.Commands;

namespace Shop.Application.Games.Commands;

public record AddEntry(Guid GameId, string Name, string TelephoneNumber) : ICommand;