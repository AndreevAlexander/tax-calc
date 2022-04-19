using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Exceptions;

namespace TaxCalculator.Cqrs.Implementation.Bus;

public class QueryBus : IQueryBus
{
    private readonly QueryHandlerResolver _queryHandlerResolver;

    public QueryBus(QueryHandlerResolver queryHandlerResolver)
    {
        _queryHandlerResolver = queryHandlerResolver;
    }

    public Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
    {
        var handler = (IQueryHandler<TQuery, TResult>)_queryHandlerResolver(typeof(TQuery), typeof(TResult));
        if (handler == null)
        {
            throw new TaxCalculatorException($"Can not retrieve handler for: {typeof(TQuery).FullName}; {typeof(TResult).FullName}");
        }

        return handler.HandleAsync(query);
    }
}