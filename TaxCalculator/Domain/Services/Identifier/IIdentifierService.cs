namespace TaxCalculator.Domain.Services.Identifier;

public interface IIdentifierService
{
    public CurrencyIdentifier<Guid> Currencies { get; }
}