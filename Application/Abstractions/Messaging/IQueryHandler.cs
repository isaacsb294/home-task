using Shared;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TQueryResponse>
    where TQuery : IQuery
{
    Task<Result<TQueryResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}