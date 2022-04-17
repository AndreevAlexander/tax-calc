namespace TaxCalculator.Validation.Contracts;

public interface IValidationProfile
{
    void ForModel<TModel>(Action<IRuleBuilder<TModel>> builder) where TModel : class;
    IRuleBuilder<TModel> GetRuleBuilder<TModel>() where TModel : class;
    bool HasRules<TModel>();
}