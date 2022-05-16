namespace TaxCalculator.WebFrontend.Models;

public class TaxProfileModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? ProfileCurrencyId { get; set; }
}