using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.AdditionalSpends.Commands;

public class AddAdditionalSpendCommand : ICommand
{
    public decimal Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
    
    public Guid TaxProfileId { get; set; }
}