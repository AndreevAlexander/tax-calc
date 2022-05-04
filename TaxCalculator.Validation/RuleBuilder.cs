using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation;

public class RuleBuilder<TModel> : IRuleBuilder<TModel>, IRuleStage where TModel : class
{
    private string PropertyName { get; set; }

    private readonly Dictionary<string, RuleConfiguration> _ruleConfigurations;
    
    public RuleBuilder()
    {
        _ruleConfigurations = new Dictionary<string, RuleConfiguration>();
    }

    public IRuleStage Property<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
    {
        var body = (MemberExpression) propertyExpression.Body;
        PropertyName = body.Member.Name;
        _ruleConfigurations.TryAdd(PropertyName, new RuleConfiguration());
        
        return this;
    }

    public IRuleStage Required()
    {
        _ruleConfigurations[PropertyName].IsRequired = true;
        return this;
    }

    public IRuleStage MinLength(int value)
    {
        _ruleConfigurations[PropertyName].MinLength = value;
        return this;
    }

    public IRuleStage MaxLength(int value)
    {
        _ruleConfigurations[PropertyName].MaxLength = value;
        return this;
    }

    public IRuleStage Regex(string pattern)
    {
        _ruleConfigurations[PropertyName].Regex = pattern;
        return this;
    }

    public IRuleStage WithCustomRule<TCustomRule>() where TCustomRule : IValidationRule
    {
        _ruleConfigurations[PropertyName].CustomValidators.Add(typeof(TCustomRule));
        return this;
    }

    public Dictionary<string, RuleConfiguration> Build()
    {
        return _ruleConfigurations;
    }
}
