using System.Collections.Generic;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules;

public class MinLengthValidationRule : IValidationRule
{
    public IEnumerable<ValidationResult> Validate(object? data, string propertyName, object? context = null)
    {
        var result = new List<ValidationResult>();
		
        var minLength = (int)context;
        if (data is string s && s.Length < minLength)
        {
            result.Add(ValidationResult.Invalid($"Value can not be less than {minLength}"));
        }

        return result;
    } 
}