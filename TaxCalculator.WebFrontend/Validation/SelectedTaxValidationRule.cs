using TaxCalculator.Validation.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.WebFrontend.Validation;

public class SelectedTaxValidationRule : IValidationRule
{
    public IEnumerable<ValidationResult> Validate(object? data, string propertyName, object? context = null)
    {
        var results = new List<ValidationResult>();
        
        if (data is string s)
        {
            var result = Guid.TryParse(s, out Guid guid);
            if (!result)
            {
                results.Add(ValidationResult.Invalid("Tax profile should be selected"));
            }
        }
        else if (data == null)
        {
            results.Add(ValidationResult.Invalid("Tax profile should be selected"));
        }

        return results;
    }
}