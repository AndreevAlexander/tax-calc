using Microsoft.EntityFrameworkCore;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Incomes.Queries;

public class GetIncomesHandler : IQueryHandler<GetIncomesQuery, List<Income>>
{
    private readonly IEntityManager _entityManager;

    public GetIncomesHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Task<List<Income>> HandleAsync(GetIncomesQuery query)
    {
        var incomes = _entityManager.GetRepository<Income>().GetMany();

        if (query.ProfileId.HasValue)
        {
            incomes = incomes.Where(x => x.TaxProfileId == query.ProfileId.Value);
        }

        if (query.Page.HasValue && query.PageSize.HasValue)
        {
            incomes = incomes.Skip(query.PageSize.Value * query.Page.Value - query.PageSize.Value).Take(query.PageSize.Value);
        }

        return incomes.ToListAsync();
    }
}