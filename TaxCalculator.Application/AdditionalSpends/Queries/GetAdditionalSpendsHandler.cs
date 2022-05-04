using TaxCalculator.Application.Extensions;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.AdditionalSpends.Queries;

public class GetAdditionalSpendsHandler : IQueryHandler<GetAdditionalSpendsQuery, List<AdditionalSpend>>
{
    private readonly IEntityManager _entityManager;

    public GetAdditionalSpendsHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public Task<List<AdditionalSpend>> HandleAsync(GetAdditionalSpendsQuery query)
    {
        var additionalSpends = _entityManager.GetRepository<AdditionalSpend>().GetMany();

        if (query.ProfileId.HasValue)
        {
            additionalSpends = additionalSpends.Where(x => x.TaxProfileId == query.ProfileId.Value);
        }

        if (query.Page.HasValue && query.PageSize.HasValue)
        {
            additionalSpends = additionalSpends.Skip(query.Page.Value * query.PageSize.Value - query.PageSize.Value)
                .Take(query.PageSize.Value);
        }

        return additionalSpends.ToListAsync();
    }
}