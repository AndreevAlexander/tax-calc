using System.Linq;

namespace TaxCalculator.Domain.Services.Identifier;

public abstract class IdentifierBase<T>
{
    public string? GetIdentifierName(T identifier)
    {
        return GetType().GetProperties().FirstOrDefault(x => x.GetValue(this)?.Equals(identifier) ?? false)?.Name;
    }

    public Dictionary<T, string> ToDictionary()
    {
        return GetType().GetProperties().ToDictionary(x => (T) x.GetValue(this), x => x.Name);
    }
}