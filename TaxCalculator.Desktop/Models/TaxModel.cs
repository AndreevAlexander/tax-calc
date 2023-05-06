using System;
using TaxCalculator.Desktop.Attributes;

namespace TaxCalculator.Desktop.Models;

public class TaxModel
{
    public Guid Id { get; set; }
    
    [GridColumn("Tax Amount")]
    public double Amount { get; set; }
    
    [GridColumn("Percentage Tax")]
    public bool IsPercentage { get; set; }
    
    [GridColumn("Applies Before")]
    public decimal? AppliesBefore { get; set; }

    [GridColumn("Type of Tax")]
    public string TaxType { get; set; }

    public Guid TaxProfileId { get; set; }
}