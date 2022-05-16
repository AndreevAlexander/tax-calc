namespace TaxCalculator.WebFrontend.Models;

public class UpdateAdditionalSpendModel
{
    public string AdditionalSpendId { get; set; }
    
    public string Amount { get; set; }

    public bool AppliedBeforeTax { get; set; }
}