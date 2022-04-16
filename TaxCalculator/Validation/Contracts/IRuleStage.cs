namespace TaxCalculator.Validation.Contracts;

public interface IRuleStage
{
    IRuleStage Required();
    IRuleStage MinLength(int value);
    IRuleStage MaxLength(int value);
    IRuleStage Regex(string pattern);
    IRuleStage WithCustomRule<TCustomRule>() where TCustomRule : class;
}