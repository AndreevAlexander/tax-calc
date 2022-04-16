using System.Text.RegularExpressions;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules;

public class RegexValidationRule : IValidationRule
{
    public IEnumerable<ValidationResult> Validate(object data, string propertyName, object? context = null)
    {
        var result = new List<ValidationResult>();
		
        var pattern = (string)context;
        if (data is string s && !Regex.IsMatch(s, pattern))
        {
            result.Add(ValidationResult.Invalid($"Value does not match {pattern}"));
        }

        return result;
    } 
}