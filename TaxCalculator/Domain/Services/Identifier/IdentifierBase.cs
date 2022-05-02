namespace TaxCalculator.Domain.Services.Identifier;

public abstract class IdentifierBase<T>
{
    public string? GetIdentifierName(T identifier)
    {
        return GetType().GetProperties().FirstOrDefault(x => x.GetValue(this).Equals(identifier)).Name;
    }
}