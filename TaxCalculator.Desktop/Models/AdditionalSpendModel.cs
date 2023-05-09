using System;
using TaxCalculator.Desktop.Attributes;

namespace TaxCalculator.Desktop.Models;

public class AdditionalSpendModel
{
    public Guid Id { get; set; }
    
    [GridColumn("Amount")]
    public decimal Amount { get; set; }

    [GridColumn("Applies Before Taxation")]
    public bool AppliedBeforeTax { get; set; }

    public Guid TaxProfileId { get; set; }
}