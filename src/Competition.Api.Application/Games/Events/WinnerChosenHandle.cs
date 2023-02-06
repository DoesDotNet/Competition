using Shop.Application.Core.Events;
using Shop.Application.Core.Providers;

namespace Shop.Application.Games.Events;

public class WinnerChosenHandle : IDomainEventHandler<WinnerChosen>
{
    private readonly ISmsSender _smsSender;

    public WinnerChosenHandle(ISmsSender smsSender)
    {
        _smsSender = smsSender;
    }
    
    public async Task Handle(WinnerChosen domainEvent, CancellationToken cancellationToken)
    {
        await _smsSender.SendSms(domainEvent.WinnerTelephoneNumber, "You've won!", cancellationToken);
    }
}