namespace TaxCalculator.Domain.Entities;

public class Tax : BaseEntity
{
    public double Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public decimal? AppliesBefore { get; set; }

    public TaxProfile TaxProfile { get; set; }
}