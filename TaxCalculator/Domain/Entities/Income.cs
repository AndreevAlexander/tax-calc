namespace TaxCalculator.Domain.Entities;

public class Income : BaseEntity
{
    public decimal Value { get; set; }

    public TaxProfile TaxProfile { get; set; }
    
    public Guid TaxProfileId { get; set; }

    public DateTime IncomeDate { get; set; }
}