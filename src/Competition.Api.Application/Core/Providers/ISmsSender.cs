namespace Shop.Application.Core.Providers;

public interface ISmsSender
{
    Task SendSms(string telephoneNumber, string message, CancellationToken cancellationToken = default);
}