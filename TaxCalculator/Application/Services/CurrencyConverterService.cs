using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Services;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Services;

public class CurrencyConverterService : ICurrencyConverterService
{
    private readonly IEntityManager _entityManager;

    public CurrencyConverterService(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public decimal ToSystem(decimal value, TaxProfile taxProfile)
    {
        value *= (decimal)taxProfile.ProfileCurrency.ExchangeRate;
        return Math.Round(value, 2);
    }

    public decimal ToCurrency(decimal value, TaxProfile taxProfile, Guid currencyId)
    {
        if (taxProfile.ProfileCurrencyId != currencyId)
        {
            var currency = _entityManager.GetRepository<Currency>()
                .GetMany()
                .FirstOrDefault(x => x.Id == currencyId);

            value /= (decimal)currency.ExchangeRate;
        }

        return Math.Round(value, 2);
    }
}