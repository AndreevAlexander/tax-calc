using System;
using TaxCalculator.Desktop.Attributes;

namespace TaxCalculator.Desktop.Models;

public class TaxProfileModel
{
    public Guid Id { get; set; }

    [GridColumn("Profile Name")]
    public string Name { get; set; }

    [GridColumn]
    public string Description { get; set; }

    [GridColumn("Currency", nameof(CurrencyModel.Name))]
    public CurrencyModel Currency { get; set; }
}