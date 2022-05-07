using System;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Domain.Enums;

namespace TaxCalculator.Application.Taxes.Commands;

public class UpdateTaxCommand : ICommand
{
    public Guid Id { get; set; }
    
    public double Amount { get; set; }
    
    public bool IsPercentage { get; set; }
    
    public decimal? AppliesBefore { get; set; }

    public TaxType TaxType { get; set; }
}