using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules;

public class MaxLengthValidationRule : IValidationRule
{
    public IEnumerable<ValidationResult> Validate(object data, string propertyName, object? context = null)
    {
        var result = new List<ValidationResult>();
		
        var maxLength = (int)context;
        if (data is string s && s.Length > maxLength)
        {
            result.Add(ValidationResult.Invalid($"Value can not greater than {maxLength}"));
        }

        return result;
    } 
}