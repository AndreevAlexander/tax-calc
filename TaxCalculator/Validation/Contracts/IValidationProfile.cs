namespace TaxCalculator.Validation.Contracts;

public interface IValidationProfile
{
    void ForModel<TModel>(Action<IRuleBuilder> builder);
    RuleBuilder GetRuleBuilder<TModel>();
    bool HasRules<TModel>();
}