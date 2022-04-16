using TaxCalculator.Validation.Result;

namespace TaxCalculator.Validation;

public class ValidationResultContainer
{
    public Dictionary<string, IEnumerable<ValidationResult>> ValidationResults { get; }

    public bool HasErrors
    {
        get => ValidationResults.Values.SelectMany(x => x).Any(x => x.State == ValidationState.Invalid);
    }

    public bool HasWarnings
    {
        get => ValidationResults.Values.SelectMany(x => x).Any(x => x.State == ValidationState.Warning);
    }
    
    public ValidationResultContainer(Dictionary<string, IEnumerable<ValidationResult>> validationResults)
    {
        ValidationResults = validationResults;
    }
}