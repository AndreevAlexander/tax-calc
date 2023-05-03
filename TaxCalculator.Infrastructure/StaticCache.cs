using TaxCalculator.Contracts;

namespace TaxCalculator.Infrastructure;

public class StaticCache : ICache
{
    private static readonly Dictionary<object, object> _entries = new ();

    public void Set(object key, object value)
    {
        _entries.TryAdd(key, value);
    }

    public bool Get<TValue>(object key, out TValue? value)
    {
        var result = _entries.TryGetValue(key, out object? v);
        value = (TValue?)v;

        return result;
    }

    public TValue GetSet<TValue>(object key, Func<TValue> setFunc)
    {
        var value = setFunc();
        Set(key, value);

        return value;
    }
}