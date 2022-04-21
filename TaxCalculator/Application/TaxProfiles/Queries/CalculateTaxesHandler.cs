using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Dtos;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Services;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesHandler : IQueryHandler<CalculateTaxesQuery, List<TaxDataItemDto>>
{
    private readonly IEntityManager _entityManager;
    private readonly ICurrencyConverterService _currencyConverterService;

    public CalculateTaxesHandler(IEntityManager entityManager,
                                 ICurrencyConverterService currencyConverterService)
    {
        _entityManager = entityManager;
        _currencyConverterService = currencyConverterService;
    }

    public async Task<List<TaxDataItemDto>> HandleAsync(CalculateTaxesQuery query)
    {
        var taxProfile = await _entityManager.GetRepository<TaxProfile>()
            .As<ITaxProfileRepository>()
            .GetOneAsync(query.ProfileId, query.Period);

        var result = new List<TaxDataItemDto>();
        if (taxProfile != null)
        {
            if (query.CurrencyId.HasValue)
            {
                taxProfile.Incomes = taxProfile.Incomes.Select(x =>
                {
                    x.Value = _currencyConverterService.ToCurrency(x.Value, taxProfile, query.CurrencyId.Value);
                    return x;
                }).ToList();

                var fixedTaxes = taxProfile.Taxes.Where(x => !x.IsPercentage).Select(x =>
                {
                    x.Amount = (double)_currencyConverterService.ToCurrency((decimal)x.Amount, taxProfile, query.CurrencyId.Value);
                    return x;
                }).ToList();

                var taxes = taxProfile.Taxes.Where(x => x.IsPercentage).ToList();
                taxes.AddRange(fixedTaxes);
                taxProfile.Taxes = taxes;
            }

            result.AddRange(taxProfile.CalculateTaxes());

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