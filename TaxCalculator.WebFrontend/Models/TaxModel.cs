using TaxCalculator.WebFrontend.Enums;

namespace TaxCalculator.WebFrontend.Models;

public class TaxModel
{
    public Guid Id { get; set; }
    
    public double? Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public decimal? AppliesBefore { get; set; }

    public Guid TaxProfileId { get; set; }

    public TaxType TaxType { get; set; }
}