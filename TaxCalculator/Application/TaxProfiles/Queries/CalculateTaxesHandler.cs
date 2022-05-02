using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Dtos;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Services;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesHandler : IQueryHandler<CalculateTaxesQuery, CalculateTaxesResult>
{
    private readonly IEntityManager _entityManager;
    private readonly ICurrencyConverterService _currencyConverterService;

    public CalculateTaxesHandler(IEntityManager entityManager,
                                 ICurrencyConverterService currencyConverterService)
    {
        _entityManager = entityManager;
        _currencyConverterService = currencyConverterService;
    }

    public async Task<CalculateTaxesResult> HandleAsync(CalculateTaxesQuery query)
    {
        var taxProfile = await _entityManager.GetRepository<TaxProfile>()
            .As<ITaxProfileRepository>()
            .GetOneAsync(query.ProfileId, query.Period);

        var result = new CalculateTaxesResult();
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

            var calculatedTaxes = new List<TaxDataItemDto>();
            calculatedTaxes.AddRange(taxProfile.CalculateTaxes());
            
            var total = new TaxTotalDto
            {
                TotalIncomeGross = calculatedTaxes.Sum(x => x.IncomeGross),
                TotalIncomeNet = calculatedTaxes.Sum(x => x.IncomeNet)
            };

            result.TaxInformation = calculatedTaxes;
            result.TaxTotal = total;
        }

        return result;
    }
}