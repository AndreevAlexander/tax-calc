using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;

public class UpdateAdditionalSpendCommand : ICommand
{
    public Guid AdditionalSpendId { get; set; }
    
    public decimal Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
}