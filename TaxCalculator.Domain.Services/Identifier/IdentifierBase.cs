using System.Linq;

namespace TaxCalculator.Domain.Services.Identifier;

public abstract class IdentifierBase<T>
{
    public Dictionary<T, string> IdentifierValues { get; private set; }

    public string this[T identifierId] => IdentifierValues[identifierId];

    public string? GetIdentifierName(T identifier)
    {
        return this[identifier];
    }

    protected void InitializeIdentifierValues()
    {
        IdentifierValues = GetType().GetProperties()
            .Where(x => x.PropertyType == typeof(T))
            .ToDictionary(x => (T) x.GetValue(this), x => x.Name);
    }
}