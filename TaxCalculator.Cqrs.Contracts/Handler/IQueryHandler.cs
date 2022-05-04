using System.Threading.Tasks;

namespace TaxCalculator.Cqrs.Contracts.Handler;

public interface IQueryHandler<TQuery, TResult> : IHandler where TQuery : IQuery
{
    Task<TResult> HandleAsync(TQuery query);
}