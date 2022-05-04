using System;
using System.Collections.Generic;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation;

public class ValidationProfile : IValidationProfile
{
	private readonly Dictionary<Type, IRuleBuilder> _rulesForModels;

	public ValidationProfile()
	{
		_rulesForModels = new Dictionary<Type, IRuleBuilder>();
	}
	
    public void ForModel<TModel>(Action<IRuleBuilder<TModel>> builder) where TModel : class
    {
	    var ruleBuilder = new RuleBuilder<TModel>();
	    builder(ruleBuilder);

	    _rulesForModels.TryAdd(typeof(TModel), ruleBuilder);
    }

    public IRuleBuilder<TModel> GetRuleBuilder<TModel>() where TModel : class
    {
	    return (IRuleBuilder<TModel>)_rulesForModels[typeof(TModel)];
    }

    public bool HasRules<TModel>()
    {
	    return _rulesForModels.ContainsKey(typeof(TModel));
    }
}