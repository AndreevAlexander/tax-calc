namespace TaxCalculator.Domain.Entities;

public class AdditionalSpend : BaseEntity
{
    public decimal Amount { get; set; }

    public bool AppliedBeforeTax { get; set; } = false;

    public TaxProfile TaxProfile { get; set; }
    
    public Guid TaxProfileId { get; set; }
}