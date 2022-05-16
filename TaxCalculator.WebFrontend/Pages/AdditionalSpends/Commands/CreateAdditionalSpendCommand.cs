using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;

public class CreateAdditionalSpendCommand : ICommand
{
    public decimal Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
    
    public Guid TaxProfileId { get; set; }
}