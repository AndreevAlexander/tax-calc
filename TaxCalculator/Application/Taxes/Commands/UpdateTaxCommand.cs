using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Taxes.Commands;

public class UpdateTaxCommand : ICommand
{
    public Guid Id { get; set; }
    
    public double Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public decimal? AppliesBefore { get; set; }
}