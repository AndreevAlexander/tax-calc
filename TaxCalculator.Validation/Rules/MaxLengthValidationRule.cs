using System.Collections.Generic;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules;

public class MaxLengthValidationRule : IValidationRule
{
    public async Task<IEnumerable<ValidationResult>> ValidateAsync(object? data, string propertyName, object? context = null)
    {
        var result = new List<ValidationResult>();

        if (context is int maxLength)
        {
            if (data is string s && s.Length > maxLength)
            {
                result.Add(ValidationResult.Invalid($"Value can not be greater than {maxLength}"));
            }
        }
        else
        {
            throw new Exception("Max length was not set");
        }

        return result;
    } 
}