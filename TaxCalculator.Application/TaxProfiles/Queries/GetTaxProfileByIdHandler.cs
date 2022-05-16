using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class GetTaxProfileByIdHandler : IQueryHandler<GetTaxProfileByIdQuery, TaxProfile?>
{
    private readonly IEntityManager _entityManager;

    public GetTaxProfileByIdHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<TaxProfile?> HandleAsync(GetTaxProfileByIdQuery query)
    {
        return await _entityManager.GetRepository<TaxProfile>().GetOneAsync(query.TaxProfileId);
    }
}