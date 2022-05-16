using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;

public class RemoveAdditionalSpendCommand : ICommand
{
    public Guid AdditionalSpendId { get; set; }
}