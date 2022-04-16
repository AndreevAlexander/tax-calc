using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts;

public interface IValidationRule
{
    IEnumerable<ValidationResult> Validate(object? data, string propertyName, object? context = null);
}