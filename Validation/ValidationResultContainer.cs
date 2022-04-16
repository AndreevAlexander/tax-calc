using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation;

public class ValidationResultContainer
{
    public List<ValidationResult> ValidationResults { get; }

    public bool HasErrors
    {
        get => ValidationResults.Any(x => x.State == ValidationState.Invalid);
    }

    public bool HasWarnings
    {
        get => ValidationResults.Any(x => x.State == ValidationState.Warning);
    }
    
    public ValidationResultContainer(List<ValidationResult> validationResults)
    {
        ValidationResults = validationResults;
    }
}