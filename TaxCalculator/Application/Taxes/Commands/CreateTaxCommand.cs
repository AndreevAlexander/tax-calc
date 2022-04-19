using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Taxes.Commands;

public class CreateTaxCommand : ICommand
{
    public double Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public decimal? AppliesBefore { get; set; }

    public Guid TaxProfileId { get; set; }
}