using TaxCalculator.Domain.Enums;

namespace TaxCalculator.WebFrontend.Models;

public class UpdateTaxModel
{
    public Guid Id { get; set; }
    
    public string? Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public string? AppliesBefore { get; set; }

    public string TaxType { get; set; }
}