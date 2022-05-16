using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.WebApi;

public class ValidatedCommandResult : CommandResult
{
    public Dictionary<string, IEnumerable<ValidationResult>> ValidationResults { get; set; }
}