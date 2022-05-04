using System;
using Microsoft.Extensions.Caching.Memory;
using TaxCalculator.Contracts;

namespace TaxCalculator.Infrastructure;

public class CacheDecorator : ICache
{
    private readonly IMemoryCache _memoryCache;

    public CacheDecorator(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Set(object key, object value)
    {
        _memoryCache.Set(key, value);
    }

    public bool Get<TValue>(object key, out TValue value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }

    public TValue GetSet<TValue>(object key, Func<TValue> setFunc)
    {
        var fail = !_memoryCache.TryGetValue(key, out TValue value);
        if (fail)
        {
            value = setFunc();
            _memoryCache.Set(key, value);
        }

        return value;
    }
}