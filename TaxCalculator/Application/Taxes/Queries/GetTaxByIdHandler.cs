using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Taxes.Queries;

public class GetTaxByIdHandler : IQueryHandler<GetTaxByIdQuery, Tax?>
{
    private readonly IEntityManager _entityManager;

    public GetTaxByIdHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<Tax?> HandleAsync(GetTaxByIdQuery query)
    {
        return await _entityManager.GetRepository<Tax>().GetOneAsync(query.TaxId);
    }
}