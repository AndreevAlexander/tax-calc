namespace TaxCalculator.Domain.Services.Identifier;

public interface IIdentifierService
{
    string GetNameByIdentifierValue<T>(IIdentifier<T> identifier, T value);
    
    public CurrencyIdentifier<Guid> Currencies { get; }
}