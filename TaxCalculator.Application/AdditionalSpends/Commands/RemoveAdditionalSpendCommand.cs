using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.AdditionalSpends.Commands;

public class RemoveAdditionalSpendCommand : ICommand
{
    public Guid AdditionalSpendId { get; set; }
}