using System.Collections;
using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Rules;

public class RequiredValidationRule : IValidationRule
{
    public IEnumerable<ValidationResult> Validate(object? data, string propertyName, object? context = null)
    {
        var hasErrors = false;
        var results = new List<ValidationResult>();

        if (data == null)
        {
            hasErrors = true;
        }
		
        if (data is string s && (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)))
        {
            hasErrors = true;
        }

        if (data is Array a && a.Length == 0)
        {
            hasErrors = true;
        }

        if (data is IEnumerable e)
        {
            var count = 0;
            foreach (var item in e)
            {
                count++;
                break;
            }

            if (count == 0)
            {
                hasErrors = true;
            }
        }

        if (data is DateTime dateTime && dateTime == DateTime.MinValue)
        {
            hasErrors = true;
        }

        if (data is Guid guid && guid == Guid.Empty)
        {
            hasErrors = true;
        }
        
        if (hasErrors)
        {
            results.Add(ValidationResult.Invalid("Value is required"));
        }

        return results;
    } 
}