namespace TaxCalculator.Cqrs.Contracts.Bus;

public interface IQueryBus
{
    Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery;
}