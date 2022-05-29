namespace TaxCalculator.WebFrontend.Models;

public class TaxProfileDropdownModel
{
    public string TaxProfileId { get; set; }

    public string? CurrencyId { get; set; }

    public DateTime From { get; set; }

    public DateTime To { get; set; }
}