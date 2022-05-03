using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Incomes.Queries;

public class GetIncomeByIdHandler : IQueryHandler<GetIncomeByIdQuery, Income?>
{
    private readonly IEntityManager _entityManager;

    public GetIncomeByIdHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<Income?> HandleAsync(GetIncomeByIdQuery query)
    {
        var income = await _entityManager.GetRepository<Income>().GetOneAsync(query.IncomeId);

        return income;
    }
}