namespace TaxCalculator.WebFrontend.Models;

public class CreateAdditionalSpendModel
{
    public string Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
    
    public string TaxProfileId { get; set; }
}