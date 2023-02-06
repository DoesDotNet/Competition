namespace Shop.Application.Core.Queries;

public interface IQueryProcessor
{
    Task<TResult?> Process<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}