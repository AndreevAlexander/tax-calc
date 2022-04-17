using Microsoft.EntityFrameworkCore;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Dtos;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesHandler : IQueryHandler<CalculateTaxesQuery, List<TaxDataItemDto>>
{
    private readonly IEntityManager _entityManager;

    public CalculateTaxesHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<List<TaxDataItemDto>> HandleAsync(CalculateTaxesQuery query)
    {
        var taxProfile = await _entityManager.GetRepository<TaxProfile>().GetMany()
            .Include(x => x.Incomes)
            .Include(x => x.Taxes)
            .Include(x => x.AdditionalSpends)
            .FirstOrDefaultAsync(x => x.Id == query.ProfileId);

        var result = new List<TaxDataItemDto>();
        if (taxProfile != null)
        {
            result.AddRange(taxProfile.CalculateTaxes(query.From, query.To));

            var total = new TaxDataItemDto
            {
                Title = "Total",
                IncomeGross = result.Sum(x => x.IncomeGross),
                IncomeNet = result.Sum(x => x.IncomeNet)
            };

            result.Add(total);
        }

        return result;
    }
}