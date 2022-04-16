using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts;

public interface IValidationEngine
{
    ValidationResultContainer Validate<TModel>(TModel model, object? context = null);
    void RegisterValidationProfile(ValidationProfile validationProfile);
}