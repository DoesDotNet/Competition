using Shop.Application.Core.Events;

namespace Shop.Application.Games.Events;

public record WinnerChosen(Guid GameId, string WinnersName, string WinnerTelephoneNumber) : IDomainEvent;