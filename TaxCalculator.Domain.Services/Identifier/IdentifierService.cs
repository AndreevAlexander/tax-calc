using System;
using System.Reflection;

namespace TaxCalculator.Domain.Services.Identifier;

public class IdentifierService : IIdentifierService
{
    public CurrencyIdentifier<Guid> Currencies { get; private set; }

    public IdentifierService()
    {
        SetUpCurrencies();
    }

    private void SetUpCurrencies()
    {
        Currencies = new CurrencyIdentifier<Guid>(Guid.Parse("FC5EE427-961D-424F-858B-A108B5EE74FE"),
                                                  Guid.Parse("77F897E4-ACA0-4F33-AD87-B630F55E1D11"),
                                                  Guid.Parse("366A90E0-6200-40AC-85A5-7951D587D7B4"),
                                                  Guid.Parse("2FF82FD1-D11C-444F-9FCA-44CB4ADB9B78"));
    }
}