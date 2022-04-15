using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.Cqrs.Implementation.Bus;

public class QueryBus : IQueryBus
{
    private readonly List<IHandler> _queryHandlers;

    public QueryBus(List<IHandler> queryHandlers)
    {
        _queryHandlers = queryHandlers;
    }

    public Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
    {
        var handler = _queryHandlers.OfType<IQueryHandler<TQuery, TResult>>().FirstOrDefault();
        if (handler == null)
        {
            throw new Exception($"Can not retrieve handler for: {typeof(TQuery).FullName}; {typeof(TResult).FullName}");
        }

        return handler.HandleAsync(query);
    }
}