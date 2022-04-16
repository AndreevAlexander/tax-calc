using Microsoft.EntityFrameworkCore;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class GetTaxProfilesHandler : IQueryHandler<GetTaxProfilesQuery, List<TaxProfile>>
{
    private readonly IEntityManager _entityManager;

    public GetTaxProfilesHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Task<List<TaxProfile>> HandleAsync(GetTaxProfilesQuery query)
    {
        var profilesQuery = _entityManager.GetRepository<TaxProfile>().GetMany();
        if (query.Page != null && query.PageSize != null)
        {
            profilesQuery = profilesQuery.Skip(query.Page.Value * query.PageSize.Value - query.PageSize.Value)
                .Take(query.PageSize.Value);
        }

        return profilesQuery.ToListAsync();
    }
}