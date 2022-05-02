using System.Reflection;

namespace TaxCalculator.Domain.Services.Identifier;

public class IdentifierService : IIdentifierService
{
    public CurrencyIdentifier<Guid> Currencies { get; private set; }

    public IdentifierService()
    {
        SetUpCurrencies();
    }

    public string GetNameByIdentifierValue<T>(IIdentifier<T> identifier, T value)
    {
        return identifier.GetType().GetProperties().FirstOrDefault(x => x.GetValue(identifier).Equals(value))?.Name;
    }

    private void SetUpCurrencies()
    {
        Currencies = new CurrencyIdentifier<Guid>
        {
            Usd = Guid.Parse("FC5EE427-961D-424F-858B-A108B5EE74FE"),
            Eur = Guid.Parse("77F897E4-ACA0-4F33-AD87-B630F55E1D11"),
            Uah = Guid.Parse("366A90E0-6200-40AC-85A5-7951D587D7B4"),
            Pln = Guid.Parse("2FF82FD1-D11C-444F-9FCA-44CB4ADB9B78"),
        };
    }
}