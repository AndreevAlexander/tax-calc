using System.Collections.Generic;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules;

public class MinLengthValidationRule : IValidationRule
{
    public async Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, string propertyName, object? context = null)
    {
        var result = new List<ValidationResult>();

        if (context is int minLength)
        {
            if (data is string s && s.Length < minLength)
            {
                result.Add(ValidationResult.Invalid($"Value can not be less than {minLength}"));
            }
        }
        else
        {
            throw new Exception("Min length was not set");
        }

        return result;
    } 
}