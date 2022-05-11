using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation.Contracts;

public interface IValidationEngine
{
    Task<ValidationResultContainer> ValidateAsync<TModel>(TModel model, object? context = null) where TModel : class;
    void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new();
}