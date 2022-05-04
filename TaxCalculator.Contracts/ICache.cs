using System;

namespace TaxCalculator.Contracts;

public interface ICache
{
    void Set(object key, object value);
    bool Get<TValue>(object key, out TValue value);
    TValue GetSet<TValue>(object key, Func<TValue> setFunc);
}