using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.AdditionalSpends.Commands;

public class UpdateAdditionalSpendCommand : ICommand
{
    public Guid AdditionalSpendId { get; set; }
    
    public decimal Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
}