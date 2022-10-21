namespace Shop.Application.Core.Commands;

public interface ICommandDispatcher
{
    Task Dispatch(ICommand command);
}