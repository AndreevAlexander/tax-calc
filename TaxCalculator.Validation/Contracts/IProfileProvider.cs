namespace TaxCalculator.Validation.Contracts;

public interface IProfileProvider
{
    IEnumerable<IValidationProfile> GetRules<TModel>() where TModel : class;
    
    void RegisterValidationProfile<TProfile>() where TProfile : ValidationProfile, new();
}