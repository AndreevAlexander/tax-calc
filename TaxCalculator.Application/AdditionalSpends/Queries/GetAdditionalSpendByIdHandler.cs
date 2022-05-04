using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.AdditionalSpends.Queries;

public class GetAdditionalSpendByIdHandler : IQueryHandler<GetAdditionalSpendByIdQuery, AdditionalSpend?>
{
    private readonly IEntityManager _entityManager;

    public GetAdditionalSpendByIdHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<AdditionalSpend?> HandleAsync(GetAdditionalSpendByIdQuery query)
    {
        return await _entityManager.GetRepository<AdditionalSpend>().GetOneAsync(query.AdditionalSpendId);
    }
}