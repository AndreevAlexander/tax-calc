namespace TaxCalculator.Validation.Contracts;

public interface IRuleBuilder
{
    IRuleStage Property(string propertyName);

    Dictionary<string, RuleConfiguration> Build();
}