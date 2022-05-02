using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Dtos;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Services;
using TaxCalculator.Domain.Services.Identifier;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesHandler : IQueryHandler<CalculateTaxesQuery, CalculateTaxesResult>
{
    private readonly IEntityManager _entityManager;
    private readonly ICurrencyConverterService _currencyConverterService;
    private readonly IIdentifierService _identifierService;

    public CalculateTaxesHandler(IEntityManager entityManager,
                                 ICurrencyConverterService currencyConverterService,
                                 IIdentifierService identifierService)
    {
        _entityManager = entityManager;
        _currencyConverterService = currencyConverterService;
        _identifierService = identifierService;
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
                ConvertMoneyValues(taxProfile, query.CurrencyId.Value);
            }

            var calculatedTaxes = taxProfile.CalculateTaxes().ToList();

            var total = new IncomeTotalDto
            {
                TotalGross = calculatedTaxes.Sum(x => x.IncomeGross),
                TotalNet = calculatedTaxes.Sum(x => x.IncomeNet)
            };

            result.TaxInformation = calculatedTaxes;
            result.IncomeTotal = total;
            result.Currency = query.CurrencyId.HasValue
                ? _identifierService.Currencies.GetIdentifierName(query.CurrencyId.Value)
                : taxProfile.ProfileCurrency.Name;
        }

        return result;
    }

    private void ConvertMoneyValues(TaxProfile taxProfile, Guid currencyId)
    {
        taxProfile.Incomes = taxProfile.Incomes.Select(x =>
        {
            x.Value = _currencyConverterService.ToCurrency(x.Value, taxProfile, currencyId);
            return x;
        }).ToList();

        var fixedTaxes = taxProfile.Taxes.Where(x => !x.IsPercentage).Select(x =>
        {
            x.Amount = (double)_currencyConverterService.ToCurrency((decimal)x.Amount, taxProfile, currencyId);
            return x;
        }).ToList();

        var taxes = taxProfile.Taxes.Where(x => x.IsPercentage).ToList();
        taxes.AddRange(fixedTaxes);
        taxProfile.Taxes = taxes;
    }
}