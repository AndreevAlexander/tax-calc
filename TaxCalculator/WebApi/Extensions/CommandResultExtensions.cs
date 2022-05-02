using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Validation.Result;
using TaxCalculator.WebApi.Dtos;

namespace TaxCalculator.WebApi.Extensions;

public static class CommandResultExtensions
{
    public static ValidatedCommandResult ToValidated(this CommandResult commandResult,
        Dictionary<string, IEnumerable<ValidationResult>> results)
    {
        return new ValidatedCommandResult
        {
            Status = commandResult.Status,
            RecordId = commandResult.RecordId,
            Warnings = results.Where(x => x.Value.All(y => y.State == ValidationState.Warning))
                .ToDictionary(x => x.Key, x => x.Value)
        };
    }
}