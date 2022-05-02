using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Validation.Result;

namespace TaxCalculator.WebApi.Dtos;

public class ValidatedCommandResult : CommandResult
{
    public Dictionary<string, IEnumerable<ValidationResult>> Warnings { get; set; }
}