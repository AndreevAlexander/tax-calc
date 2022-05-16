using System;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Domain.Enums;

namespace TaxCalculator.Application.Taxes.Commands;

public class CreateTaxCommand : ICommand
{
    public double Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public decimal? AppliesBefore { get; set; }

    public Guid TaxProfileId { get; set; }

    public TaxType TaxType { get; set; }
}