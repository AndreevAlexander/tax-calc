namespace TaxCalculator.WebFrontend.Models;

public class CreateTaxProfileModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string ProfileCurrencyId { get; set; }
}