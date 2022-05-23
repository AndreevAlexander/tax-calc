namespace TaxCalculator.WebFrontend.Models;

public class AdditionalSpendModel
{
    public Guid Id { get; set; }
    
    public decimal? Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
    
    public Guid TaxProfileId { get; set; }
}