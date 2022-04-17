using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts;

public interface IValidationEngine
{
    ValidationResultContainer Validate<TModel>(TModel model, object? context = null) where TModel : class;
    void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new();
}