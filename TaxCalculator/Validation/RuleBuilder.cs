using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.Validation;

public class RuleBuilder : IRuleBuilder, IRuleStage
{
    private string PropertyName { get; set; }

    private readonly Dictionary<string, RuleConfiguration> _ruleConfigurations;
    public RuleBuilder()
    {
        _ruleConfigurations = new Dictionary<string, RuleConfiguration>();
    }
	
    public IRuleStage Property(string propertyName)
    {
        PropertyName = propertyName;
        _ruleConfigurations.TryAdd(propertyName, new RuleConfiguration());
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

    public IRuleStage WithCustomRule<TCustomRule>() where TCustomRule : class
    {
        _ruleConfigurations[PropertyName].CustomValidators.Add(typeof(TCustomRule));
        return this;
    }

    public Dictionary<string, RuleConfiguration> Build()
    {
        return _ruleConfigurations;
    }
}
