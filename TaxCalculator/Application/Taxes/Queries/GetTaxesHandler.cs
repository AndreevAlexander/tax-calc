using Microsoft.EntityFrameworkCore;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Taxes.Queries;

public class GetTaxesHandler : IQueryHandler<GetTaxesQuery, List<Tax>>
{
    private readonly IEntityManager _entityManager;

    public GetTaxesHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Task<List<Tax>> HandleAsync(GetTaxesQuery query)
    {
        var taxes = _entityManager.GetRepository<Tax>().GetMany();
        
        if (query.ProfileId.HasValue)
        {
            taxes = taxes.Where(x => x.TaxProfileId == query.ProfileId.Value);
        }
        
        if (query.Page != null && query.PageSize != null)
        {
            taxes = taxes.Skip(query.Page.Value * query.PageSize.Value - query.PageSize.Value)
                .Take(query.PageSize.Value);
        }

        return taxes.ToListAsync();
    }
}