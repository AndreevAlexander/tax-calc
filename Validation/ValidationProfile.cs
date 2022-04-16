using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation;

public class ValidationProfile : IValidationProfile
{
	private readonly Dictionary<Type, RuleBuilder> _rulesForModels;

	public ValidationProfile()
	{
		_rulesForModels = new Dictionary<Type, RuleBuilder>();
	}
	
    public void ForModel<TModel>(Action<IRuleBuilder> builder)
    {
	    var ruleBuilder = new RuleBuilder();
	    builder(ruleBuilder);

	    _rulesForModels.TryAdd(typeof(TModel), ruleBuilder);
    }

    public RuleBuilder GetRuleBuilder<TModel>()
    {
	    return _rulesForModels[typeof(TModel)];
    }

    public bool HasRules<TModel>()
    {
	    return _rulesForModels.ContainsKey(typeof(TModel));
    }
}