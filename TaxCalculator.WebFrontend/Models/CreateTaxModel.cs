using TaxCalculator.Domain.Enums;

namespace TaxCalculator.WebFrontend.Models;

public class CreateTaxModel
{
    public string Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public string? AppliesBefore { get; set; }

    public Guid TaxProfileId { get; set; }

    public TaxType TaxType { get; set; }
}