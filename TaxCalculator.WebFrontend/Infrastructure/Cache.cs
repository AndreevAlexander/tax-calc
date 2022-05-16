using TaxCalculator.Contracts;

namespace TaxCalculator.WebFrontend.Infrastructure;

public class Cache : ICache
{
    private Dictionary<object, object> _entries;

    public Cache()
    {
        _entries = new Dictionary<object, object>();
    }
    
    public void Set(object key, object value)
    {
        _entries.TryAdd(key, value);
    }

    public bool Get<TValue>(object key, out TValue? value)
    {
        var result = _entries.TryGetValue(key, out object? v);

        value = default;
        if (result && v != null)
        {
            value = (TValue) v;
        }
        
        return result;
    }

    public TValue GetSet<TValue>(object key, Func<TValue> setFunc)
    {
        if (!_entries.ContainsKey(key))
        {
            Set(key, setFunc());
        }
        
        Get(key, out TValue v);

        return v;
    }
}