namespace Shop.Application.Core.Providers;

public class PretendSmsProvider : ISmsSender
{
    public Task SendSms(string telephoneNumber, string message, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}