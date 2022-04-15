using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation;

public interface IValidationEngine
{
    ValidationResult[] Validate<TModel>(TModel model, object? context = null);
}